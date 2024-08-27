using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class ReviewerService : ServiceBase<ReviewerService>
	{
		private readonly IConfiguration configuration;
		public ReviewerService(AlFikrContext alFikrContext, ILogger<ReviewerService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public IEnumerable<ReviewerEntity> GetReviewers()
		{
			var reviewers = new List<ReviewerEntity>();
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					using (var command = new MySqlCommand("SELECT reviewer.Id, reviewer.reviewerName, reviewer.reviewerArName, reviewer.AddedOn FROM reviewer ORDER BY reviewer.Id DESC", connection))
					{
						using (var reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								reviewers.Add(new ReviewerEntity
								{
									Id = reader.GetInt32("Id"),
									reviewerName = reader.GetString("reviewerName"),
									reviewerArName = reader.GetString("reviewerArName"),
									AddedOn = reader.GetDateTime("AddedOn")
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
			return reviewers;
		}

		public ReviewerEntity GetReviewer(int id)
		{
			ReviewerEntity reviewer = null;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					using (var command = new MySqlCommand("SELECT reviewer.Id, reviewer.reviewerName, reviewer.reviewerArName, reviewer.AddedOn FROM reviewer WHERE reviewer.Id = @Id LIMIT 1", connection))
					{
						command.Parameters.AddWithValue("@Id", id);
						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								reviewer = new ReviewerEntity
								{
									Id = reader.GetInt32("Id"),
									reviewerName = reader.IsDBNull(reader.GetOrdinal("reviewerName")) ? null : reader.GetString("reviewerName"),
									reviewerArName = reader.IsDBNull(reader.GetOrdinal("reviewerArName")) ? null : reader.GetString("reviewerArName"),
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
			return reviewer;
		}

		public List<ThesisEntity> GetThesesByReviewerId(int reviewerId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var theses = connection.Query<ThesisEntity>(@"SELECT Document.*, Thesis.Speciality, Thesis.Discipline, Thesis.Institution, Thesis.University, Thesis.Type, Thesis.DefenceDate, Thesis.NbPages 
                                                                  FROM Thesis 
                                                                  JOIN thesisreviewer ON Thesis.Id = thesisreviewer.ThesisId 
                                                                  JOIN Document ON Thesis.Id = Document.Id
                                                                  WHERE thesisreviewer.reviewerId = @reviewerId",
																  new { reviewerId = reviewerId }).ToList();
					return theses;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int AddReviewer(ReviewerEntity reviewer)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "INSERT INTO reviewer (reviewerName, reviewerArName, AddedOn) VALUES (@reviewerName, @reviewerArName, @AddedOn);";
					using (var command = new MySqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@reviewerName", reviewer.reviewerName);
						command.Parameters.AddWithValue("@reviewerArName", reviewer.reviewerArName);
						command.Parameters.AddWithValue("@AddedOn", reviewer.AddedOn);
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

		public int UpdateReviewer(ReviewerEntity reviewer)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "UPDATE reviewer SET reviewerName = @reviewerName, reviewerArName = @reviewerArName, AddedOn = @AddedOn WHERE Id = @Id; ";
					using (var command = new MySqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@reviewerName", reviewer.reviewerName);
						command.Parameters.AddWithValue("@reviewerArName", reviewer.reviewerArName);
						command.Parameters.AddWithValue("@AddedOn", reviewer.AddedOn);
						command.Parameters.AddWithValue("@Id", reviewer.Id);
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

		public int DeleteReviewer(int id)
		{
			int result = 0;
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					connection.Open();
					string sql = "DELETE FROM reviewer WHERE Id = @Id";
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
