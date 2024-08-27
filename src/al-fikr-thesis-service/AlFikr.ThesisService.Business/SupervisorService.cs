using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class SupervisorService : ServiceBase<SupervisorService>
	{
		private readonly IConfiguration configuration;
		public SupervisorService(AlFikrContext alFikrContext, ILogger<SupervisorService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public IEnumerable<SupervisorEntity> GetSupervisors()
		{
			var supervisors = new List<SupervisorEntity>();
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					using (var command = new MySqlCommand("SELECT Supervisor.Id, Supervisor.SupervisorName, Supervisor.SupervisorArName,  Supervisor.SupervisorTitle, Supervisor.AddedOn FROM Supervisor ORDER BY Supervisor.Id DESC", connection))
					{
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								supervisors.Add(new SupervisorEntity
								{
									Id = reader.GetInt32("Id"),
									//gerer le cas de null , Update (26/08/2024) 
									 SupervisorName = reader.IsDBNull(reader.GetOrdinal("SupervisorName")) ? null : reader.GetString("SupervisorName"),
                                    SupervisorArName = reader.IsDBNull(reader.GetOrdinal("SupervisorArName")) ? null : reader.GetString("SupervisorArName"),
                                    SupervisorTitle = reader.IsDBNull(reader.GetOrdinal("SupervisorTitle")) ? null : reader.GetString("SupervisorTitle"),
                                    AddedOn = reader.IsDBNull(reader.GetOrdinal("AddedOn")) ? (DateTime?)null : reader.GetDateTime("AddedOn")
             //end
								});
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
			return supervisors;
		}

		public SupervisorEntity GetSupervisor(int id)
		{
			SupervisorEntity supervisor = null;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					using (var command = new MySqlCommand("SELECT Supervisor.Id, Supervisor.SupervisorName, Supervisor.SupervisorArName, Supervisor.SupervisorTitle, Supervisor.AddedOn FROM Supervisor WHERE Supervisor.Id = @Id LIMIT 1", connection))
					{
						command.Parameters.AddWithValue("@Id", id);
						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								supervisor = new SupervisorEntity
								{
									Id = reader.GetInt32("Id"),
									SupervisorName = reader.IsDBNull(reader.GetOrdinal("SupervisorName")) ? null : reader.GetString("SupervisorName"),
									SupervisorArName = reader.IsDBNull(reader.GetOrdinal("SupervisorArName")) ? null : reader.GetString("SupervisorArName"),
									SupervisorTitle = reader.IsDBNull(reader.GetOrdinal("SupervisorTitle")) ? null : reader.GetString("SupervisorTitle"),
									AddedOn = reader.GetDateTime("AddedOn")
								};
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
			return supervisor;
		}

		public List<ThesisEntity> GetThesesBySupervisorId(int supervisorId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var theses = connection.Query<ThesisEntity>(@"SELECT Document.*, Thesis.Speciality, Thesis.Discipline, Thesis.Institution, Thesis.University, Thesis.Type, Thesis.DefenceDate, Thesis.NbPages 
                                                                  FROM Thesis 
                                                                  JOIN thesisSupervisor ON Thesis.Id = thesisSupervisor.ThesisId 
                                                                  JOIN Document ON Thesis.Id = Document.Id
                                                                  WHERE thesisSupervisor.SupervisorId = @SupervisorId",
																  new { SupervisorId = supervisorId }).ToList();
					return theses;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int AddSupervisor(SupervisorEntity supervisor)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "INSERT INTO Supervisor (SupervisorName, SupervisorArName, SupervisorTitle, AddedOn) VALUES (@SupervisorName, @SupervisorArName, @SupervisorTitle, @AddedOn); SELECT LAST_INSERT_ID();";
					using (var command = new MySqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@SupervisorName", supervisor.SupervisorName);
						command.Parameters.AddWithValue("@SupervisorArName", supervisor.SupervisorArName);
						command.Parameters.AddWithValue("@SupervisorTitle", supervisor.SupervisorTitle);
						command.Parameters.AddWithValue("@AddedOn", supervisor.AddedOn);
						result = Convert.ToInt32(command.ExecuteScalar());
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
			return result;
		}

		public int UpdateSupervisor(SupervisorEntity supervisor)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "UPDATE Supervisor SET SupervisorName = @SupervisorName, SupervisorArName = @SupervisorArName, SupervisorTitle = @SupervisorTitle, AddedOn = @AddedOn WHERE Id = @Id; ";
					using (var command = new MySqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@SupervisorName", supervisor.SupervisorName);
						command.Parameters.AddWithValue("@SupervisorArName", supervisor.SupervisorArName);
						command.Parameters.AddWithValue("@SupervisorTitle", supervisor.SupervisorTitle);
						command.Parameters.AddWithValue("@AddedOn", supervisor.AddedOn);
						command.Parameters.AddWithValue("@Id", supervisor.Id);
						result = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
			return result;
		}

		public int DeleteSupervisor(int id)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "DELETE FROM Supervisor WHERE Id = @Id";
					using (var command = new MySqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Id", id);
						result = command.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
			return result;
		}
	}
}
