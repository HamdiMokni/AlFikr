using AlFikr.ThesisService.Business.Extensions;
using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business;

public class EditorService : ServiceBase<EditorService>
{
	private readonly IConfiguration configuration;
	private readonly IMapper _mapper;
	private readonly EmailService _emailService;

	public EditorService(AlFikrContext alFikrContext, EmailService emailService, ILogger<EditorService> logger, IConfiguration configuration, IMapper mapper) : base(alFikrContext, logger)
	{
		this.configuration = configuration;
		_mapper = mapper;
		_emailService = emailService;
	}

	public IEnumerable<EditorEntity> GetEditors()
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				return connection.Query<EditorEntity>("SELECT * FROM Editor");
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public EditorEntity GetEditor(int id)
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				var editor = connection.QueryFirstOrDefault<EditorEntity>("SELECT * FROM Editor WHERE Id = @Id", new { Id = id });
				editor.MainUser = connection.QueryFirstOrDefault<UserEntity>("SELECT * FROM Users WHERE IdAffiliation = @Id AND AccountType = 'Editor' AND RoleInAffiliation = 'Administrator'", new { Id = id });

				return editor;
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public int AddEditor(EditorEntity editorEntity)
	{
		var userFromDB = alFikrContext.Users
			.Where(x => x.Email == editorEntity.MainUser.Email)
			.FirstOrDefault();

		using var trans = alFikrContext.Database.BeginTransaction();

		try
		{
			Editor editor = _mapper.Map<Editor>(editorEntity);
			User user = _mapper.Map<User>(editorEntity.MainUser);

			alFikrContext.Editors.Add(editor);
			alFikrContext.SaveChanges();

			ManageEditorUser(editor, user);
			trans.Commit();

			return editor.Id;
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			trans.Rollback();
			throw;
		}
	}

	public int UpdateEditor(EditorEntity editorEntity)
	{
		using var trans = alFikrContext.Database.BeginTransaction();
		try
		{
			Editor editor = _mapper.Map<Editor>(editorEntity);
			User user = _mapper.Map<User>(editorEntity.MainUser);

			alFikrContext.Editors.Update(editor);
			alFikrContext.SaveChanges();

			var userFromDB = alFikrContext.Users
				.Where(x => x.AccountType == "Editor" && x.IdAffiliation == editor.Id)
				.FirstOrDefault();

			if (string.IsNullOrEmpty(user.Email) && userFromDB != null)
			{
				alFikrContext.Users.Remove(userFromDB);
				alFikrContext.SaveChanges();
			}

			if (!string.IsNullOrEmpty(user.Email) && userFromDB != null && user.Email != userFromDB.Email)
			{
				alFikrContext.Users.Remove(userFromDB);
				alFikrContext.SaveChanges();
			}

			if (!string.IsNullOrEmpty(user.Email))
				ManageEditorUser(editor, user);

			trans.Commit();
			return editor.Id;
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			trans.Rollback();
			throw;
		}
	}

	private void ManageEditorUser(Editor editor, User user)
	{
		user.IdAffiliation = editor.Id;
		Random Alea = new();

		user.Code = Alea.Next(10000, 99999).ToString();
		user.EmailConfirmationCode = Alea.Next(100000, 999999).ToString();
		user.CodeExpirationDate = DateTime.Now.AddDays(1);
		user.Status = "Activated";
		user.RoleInAffiliation = "Administrator";
		user.Password = PasswordExtensions.Encrypt(PasswordExtensions.RandomPassword(), user.Code);

		alFikrContext.Users.Add(user);
		alFikrContext.SaveChanges();

		List<User> users = alFikrContext.Users
			.Where(x => x.Email == user.Email)
			.ToList();

		foreach (var item in users)
		{
			item.Code = user.Code;
			item.Password = user.Password;
			item.EmailConfirmed = user.EmailConfirmed;
			item.EmailConfirmationCode = user.EmailConfirmationCode;
			item.CodeExpirationDate = user.CodeExpirationDate;
		}

		alFikrContext.Users.UpdateRange(users);

		if (user.Id > 0)
			_emailService.SendEmail(user);
	}

	public int DeleteEditor(int id)
	{
		int res = 0;

		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				string sql = @"DELETE FROM Editor WHERE Id = @Id";

				res = connection.Execute(sql, new { Id = id });

				if (res > 0)
					connection.Execute(@"DELETE FROM Users WHERE AccountType = 'Editor' AND IdAffiliation = @Id", new { Id = id });
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}

		return res;
	}
}