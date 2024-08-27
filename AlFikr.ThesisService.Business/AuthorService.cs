using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business;

public class AuthorService : ServiceBase<AuthorService>
{
	private readonly IConfiguration configuration;
	private readonly IMapper _mapper;

	public AuthorService(AlFikrContext alFikrContext, ILogger<AuthorService> logger, IConfiguration configuration, IMapper mapper) : base(alFikrContext, logger)
	{
		this.configuration = configuration;
		_mapper = mapper;
	}

	public IEnumerable<AuthorEntity> GetAuthors()
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				return connection.Query<AuthorEntity>("SELECT * FROM Author");
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public AuthorEntity GetAuthor(int id)
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				return connection.QueryFirstOrDefault<AuthorEntity>("SELECT * FROM Author WHERE Id = @Id", new { Id = id });
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public int AddAuthor(AuthorEntity authorEntity)
	{
		try
		{
			Author author = _mapper.Map<Author>(authorEntity);

			alFikrContext.Authors.Add(author);
			alFikrContext.SaveChanges();

			return author.Id;
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public int UpdateAuthor(AuthorEntity author)
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				string sql = @" UPDATE Author 
                                SET IdEditor=@IdEditor,
                                    OrcId=@OrcId,
                                    FirstName=@FirstName,
                                    LastName=@LastName,
                                    ArName=@ArName,
                                    DateOfBirth=@DateOfBirth,
                                    Civility=@Civility,
                                    City=@City,
                                    Adress=@Adress,
                                    PostalCode=@PostalCode,
                                    Country=@Country,
                                    Position=@Position,
                                    Email=@Email,
                                    Biography=@Biography,
                                    Phone=@Phone,
                                    Photo=@Photo 
                                WHERE Id=@Id";

				return connection.Execute(sql, author);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}

	public int DeleteAuthor(int id)
	{
		try
		{
			using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
			{
				string sql = @"DELETE FROM Author WHERE Id = @Id";

				return connection.Execute(sql, new { Id = id });
			}
		}
		catch (Exception ex)
		{
			_logger.LogError($"{ex.Message} \n", ex);
			throw;
		}
	}
}
