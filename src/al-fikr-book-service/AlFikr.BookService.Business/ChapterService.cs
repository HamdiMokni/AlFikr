using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace AlFikr.BookService.Business
{
    public class ChapterService : ServiceBase<ChapterService>
    {
        private readonly IConfiguration configuration;

        public ChapterService(AlFikrContext alFikrContext, ILogger<ChapterService> logger, IConfiguration configuration) : base(alFikrContext, logger)
        {
            this.configuration = configuration;
        }

        public IEnumerable<ChapterEntity> GetChapters()
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.Query<ChapterEntity>("SELECT * FROM Chapter");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public ChapterEntity GetChapter(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.QueryFirstOrDefault<ChapterEntity>("SELECT * FROM Chapter WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int AddChapter(ChapterEntity author)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" INSERT INTO Chapter(IdDocument,Title,ChapterNumber,StartPage,EndPage,Description)
                                    VALUES(@IdDocument,@Title,@ChapterNumber,@StartPage,@EndPage,@Description)";

                    return connection.Execute(sql, author);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int UpdateChapter(ChapterEntity author)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" UPDATE Chapter 
                                    SET IdDocument=@IdDocument,
                                        Title=@Title,
                                        ChapterNumber=@ChapterNumber,
                                        StartPage=@StartPage,
                                        EndPage=@EndPage,
                                        Description=@Description
                                    WHERE Id=@Id";

                    return connection.Execute(sql, author);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int DeleteChapter(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @"DELETE FROM Chapter WHERE Id = @Id";

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
