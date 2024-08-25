using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;


namespace AlFikr.ThesisService.Business
{
	public class DocumentFilesService : ServiceBase<DocumentFilesService>
	{
		private readonly IConfiguration configuration;

		public DocumentFilesService(AlFikrContext alFikrContext, ILogger<DocumentFilesService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public IEnumerable<DocumentFilesEntity> GetDocumentFiles()
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.Query<DocumentFilesEntity>("SELECT * FROM DocumentFiles");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public DocumentFilesEntity GetDocumentFile(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.QueryFirstOrDefault<DocumentFilesEntity>("SELECT * FROM DocumentFiles WHERE Id = @Id", new { Id = id });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int AddDocumentFile(DocumentFilesEntity author)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" INSERT INTO DocumentFiles(IdDocument,Reference,Title,FileDocument,FileFormat,StartPage,EndPage,State)
                                    VALUES(@IdDocument,@Reference,@Title,@FileDocument,@FileFormat,@StartPage,@EndPage,@State);";

					return connection.Execute(sql, author);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int UpdateDocumentFile(DocumentFilesEntity documentFiles)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" UPDATE DocumentFiles 
                                    SET IdDocument=@IdDocument,
                                        Reference=@Reference,
                                        Title=@Title,
                                        FileDocument=@FileDocument,
                                        FileFormat=@FileFormat,
                                        StartPage=@StartPage,
                                        EndPage=@EndPage,
                                        State=@State 
                                    WHERE Id=@Id;";

					return connection.Execute(sql, documentFiles);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int DeleteDocumentFiles(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @"DELETE FROM DocumentFiles WHERE Id = @Id";

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
