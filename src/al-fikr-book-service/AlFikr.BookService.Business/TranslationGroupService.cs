using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.BookService.Business;

public class TranslationGroupService : ServiceBase<TranslationGroupService>
{
    private readonly IConfiguration configuration;
    
    public TranslationGroupService(AlFikrContext alFikrContext, ILogger<TranslationGroupService> logger, IConfiguration configuration) : base(alFikrContext, logger)
    {
        this.configuration = configuration;
    }

    public IEnumerable<TranslationGroupEntity> GetTranslationGroups()
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                return connection.Query<TranslationGroupEntity>("SELECT * FROM TranslationGroup");
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public TranslationGroupEntity GetTranslationGroup(int id)
    {
        try
        {
            using(var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                return connection.QueryFirstOrDefault<TranslationGroupEntity>("SELECT * FROM TranslationGroup WHERE Id = @Id", new {Id = id});
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int AddTranslationGroup(TranslationGroupEntity translation)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @" INSERT INTO TranslationGroup(IdDocument, IdGroupRefs)
                                VALUES(@IdDocument, @IdGroupRefs)";

                return connection.Execute(sql, translation);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int UpdateTranslationGroup(TranslationGroupEntity translation)
    {
        try
        {
            using(var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @" UPDATE TranslationGroup 
                                SET IdDocument=@IdDocument,
                                    IdGroupRefs=@IdGroupRefs
                                WHERE Id=@Id";

                return connection.Execute(sql, translation);
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int DeleteTranslationGroup(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @"DELETE FROM TranslationGroup WHERE Id = @Id";

                return connection.Execute(sql, new {Id = id});
            }
        }
        catch(Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }
}
