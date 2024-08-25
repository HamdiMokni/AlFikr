using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class ThesisSupervisorServices : ServiceBase<ThesisSupervisorServices>
	{
		private readonly IConfiguration configuration;
		public ThesisSupervisorServices(AlFikrContext alFikrContext, ILogger<ThesisSupervisorServices> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public List<SupervisorEntity> GetSupervisorsByThesisId(int thesisId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.Query<SupervisorEntity>(@"select
																		Supervisor.*
																	from
																		Supervisor
																	join thesisSupervisor on
																		Supervisor.Id = thesisSupervisor.SupervisorId
																	where
																		thesisSupervisor.ThesisId = @ThesisId", new { ThesisId = thesisId }).ToList(); ;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int AddThesisSupervisor(int thesisId, int supervisorId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var sql = @"INSERT INTO thesisSupervisor (thesisId, supervisorId) 
                                VALUES (@thesisId, @supervisorId);";

					return connection.Execute(sql, new { thesisId, supervisorId });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int RemoveSupervisorsByThesisId(int thesisId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var sql = @"DELETE FROM thesisSupervisor WHERE ThesisId = @ThesisId";

					var rowsAffected = connection.Execute(sql, new { ThesisId = thesisId });
					return rowsAffected;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int RemoveSupervisorsBySupervisorId(int supervisorId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var sql = @"DELETE FROM thesisSupervisor WHERE SupervisorId = @SupervisorId";

					var rowsAffected = connection.Execute(sql, new { SupervisorId = supervisorId });
					return rowsAffected;
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

