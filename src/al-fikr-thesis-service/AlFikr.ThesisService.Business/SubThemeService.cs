using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class SubThemeService : ServiceBase<SubThemeService>
	{
		private readonly IConfiguration configuration;

		public SubThemeService(AlFikrContext alFikrContext, ILogger<SubThemeService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public IEnumerable<SubThemeEntity> GetSubThemes()
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.Query<SubThemeEntity>("SELECT * FROM SubTheme");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public SubThemeEntity GetSubTheme(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.QueryFirstOrDefault<SubThemeEntity>("SELECT * FROM SubTheme WHERE Id = @Id", new { Id = id });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int AddSubTheme(SubThemeEntity subTheme)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" INSERT INTO SubTheme(IdTheme, IdCollection, Title, ArTitle, ShortTitle, Description)
                                VALUES(@IdTheme, @IdCollection, @Title, @ArTitle, @ShortTitle, @Description)";

					return connection.Execute(sql, subTheme);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int UpdateSubTheme(SubThemeEntity subTheme)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" UPDATE SubTheme 
                                SET IdTheme=@IdTheme,
                                    IdCollection=@IdCollection,
                                    Title=@Title,
                                    ArTitle=@ArTitle,
                                    ShortTitle=@ShortTitle,
                                    Description=@Description
                                WHERE Id=@Id";

					return connection.Execute(sql, subTheme);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int DeleteSubTheme(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @"DELETE FROM SubTheme WHERE Id = @Id";

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

}

