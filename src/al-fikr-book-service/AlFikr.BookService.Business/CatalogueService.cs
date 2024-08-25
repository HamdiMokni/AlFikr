using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.BookService.Business
{
    public class CatalogueService : ServiceBase<CatalogueService>
    {
        private readonly IConfiguration configuration;

        public CatalogueService(AlFikrContext alFikrContext, ILogger<CatalogueService>logger, IConfiguration configuration) : base(alFikrContext, logger)
        {
            this.configuration = configuration;
        }

        public IEnumerable<CatalogueEntity> GetCatalogues()
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.Query<CatalogueEntity>("SELECT * FROM Catalogue");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public CatalogueEntity GetCatalogue(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.QueryFirstOrDefault<CatalogueEntity>("SELECT * FROM Catalogue WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public int AddCatalogue(CatalogueEntity catalogue)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" INSERT INTO Catalogue (Title, ArTitle, IdOwner, OwnerName, OwnerType, ShortTitle, Description)
                                    VALUES (@Title, @ArTitle, @IdOwner, @OwnerName, @OwnerType, @ShortTitle, @Description);";

                    return connection.Execute(sql, catalogue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public int UpdateCatalogue(CatalogueEntity catalogue)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" UPDATE Catalogue
                                    SET Title = @Title,
                                        ArTitle = @ArTitle,
                                        IdOwner = @IdOwner,
                                        OwnerName = @OwnerName,
                                        OwnerType = @OwnerType,
                                        ShortTitle = @ShortTitle,
                                        Description = @Description
                                    WHERE Id = @Id;";

                    return connection.Execute(sql, catalogue);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public int DeleteCatalogue(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @"DELETE FROM Catalogue WHERE Id = @Id";

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
