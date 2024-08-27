using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class CollectionService : ServiceBase<CollectionService>
	{
		private readonly IConfiguration configuration;

		public CollectionService(AlFikrContext alFikrContext, ILogger<CollectionService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public IEnumerable<CollectionEntity> GetCollections()
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.Query<CollectionEntity>("SELECT * FROM Collection");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public CollectionEntity GetCollection(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.QueryFirstOrDefault<CollectionEntity>("SELECT * FROM Collection WHERE Id = @Id", new { Id = id });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int AddCollection(CollectionEntity collection)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" INSERT INTO Collection (IdEditor, Title, ArTitle, ShortTitle, Description)
                                    VALUES (@IdEditor, @Title, @ArTitle, @ShortTitle, @Description);";

					return connection.Execute(sql, collection);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int UpdateCollection(CollectionEntity collection)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" UPDATE Collection
                                    SET IdEditor = @IdEditor, 
                                        Title = @Title, 
                                        ArTitle = @ArTitle, 
                                        ShortTitle = @ShortTitle, 
                                        Description = @Description
                                    WHERE Id = @Id";

					return connection.Execute(sql, collection);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public int DeleteCollection(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @"DELETE FROM Collection WHERE Id = @Id";

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
