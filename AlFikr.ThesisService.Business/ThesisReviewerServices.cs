using AlFikr.ThesisService.Data.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class ThesisReviewerServices : ServiceBase<ThesisSupervisorServices>
	{
		private readonly IConfiguration configuration;
		public ThesisReviewerServices(AlFikrContext alFikrContext, ILogger<ThesisSupervisorServices> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}

		public int RemoveReviewersByThesisId(int thesisId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var sql = @"DELETE FROM thesisReviewer WHERE ThesisId = @ThesisId";

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
		public int RemoveReviewersByReviewerId(int ReviewerId)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					var sql = @"DELETE FROM thesisReviewer WHERE ReviewerId = @ReviewerId";

					var rowsAffected = connection.Execute(sql, new { ReviewerId = ReviewerId });
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

