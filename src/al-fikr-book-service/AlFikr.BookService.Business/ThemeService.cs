using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.BookService.Business;

public class ThemeService : ServiceBase<ThemeService>
{
    private readonly IConfiguration configuration;

    public ThemeService(AlFikrContext alFikrContext, ILogger<ThemeService> logger, IConfiguration configuration) : base(alFikrContext, logger)
    {
        this.configuration = configuration;
    }

    public IEnumerable<ThemeEntity> GetThemes()
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                return connection.Query<ThemeEntity>("SELECT * FROM Theme");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public ThemeEntity GetTheme(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                return connection.QueryFirstOrDefault<ThemeEntity>("SELECT * FROM Theme WHERE Id = @Id", new { Id = id });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int AddTheme(ThemeEntity theme)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @" INSERT INTO Theme(Title, ArTitle, ShortTitle, Description)
                                VALUES(@Title, @ArTitle, @ShortTitle, @Description)";

                return connection.Execute(sql, theme);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int UpdateTheme(ThemeEntity theme)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @" UPDATE Theme 
                                SET Title=@Title,
                                    ArTitle=@ArTitle,
                                    ShortTitle=@ShortTitle,
                                    Description=@Description
                                WHERE Id=@Id";

                return connection.Execute(sql, theme);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.Message} \n", ex);
            throw;
        }
    }

    public int DeleteTheme(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
            {
                string sql = @"DELETE FROM Theme WHERE Id = @Id";

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
