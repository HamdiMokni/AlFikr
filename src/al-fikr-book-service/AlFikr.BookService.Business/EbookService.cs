using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.BookService.Business
{
    public class EbookService : ServiceBase<EbookService>
    {
        private readonly IConfiguration configuration;
        public EbookService(AlFikrContext alFikrContext, ILogger<EbookService> logger, IConfiguration configuration) : base(alFikrContext, logger)
        {
            this.configuration = configuration;
        }
        public IEnumerable<EbookEntity> GetEbooks()
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.Query<EbookEntity>(@"SELECT Document.Id,
                                                                Document.IdEditor,
                                                                Document.IdCollection,
                                                                Document.Doi,
                                                                Document.FileDocument,
                                                                Document.MarcRecordNumber,
                                                                Document.OriginalTitle,
                                                                Document.TitlesVariants,
                                                                Document.Subtitle,
                                                                Document.Foreword,
                                                                Document.Keywords,
                                                                Document.FileFormat,
                                                                Document.CoverPage,
                                                                Document.Url,
                                                                Document.DocumentType,
                                                                Document.OriginalLanguage,
                                                                Document.LanguagesVariants,
                                                                Document.Translator,
                                                                Document.AccessType,
                                                                Document.State,
                                                                Document.Price,
                                                                Document.PublicationDate,
                                                                Document.Country,
                                                                Document.PhysicalDescription,
                                                                Document.AccompanyingMaterials,
                                                                Document.AccompanyingMaterialsNb,
                                                                Document.VolumeNb,
                                                                Document.IsMultiVolume,
                                                                Document.DocumentVolumeRefs,
                                                                Document.MarcFile,
                                                                Document.BlankPages,
                                                                Document.Abstract,
                                                                Document.Notes,
                                                                Document.Notes,
                                                                Document.IdOriginal,
                                                                Document.IsTranslated,
                                                                Ebook.EditionNum,
                                                                Ebook.EditionPlace,
                                                                Ebook.ISBN,
                                                                Ebook.Genre,
                                                                Ebook.Category,
                                                                Ebook.NbPages
                                                        FROM Ebook
                                                                    LEFT JOIN Document ON Ebook.Id = Document.Id
                                                        WHERE (Document.State = 'published' OR Document.State = 'unpublished')
                                                        ORDER BY Ebook.Id DESC");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public EbookEntity GetEbook(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.QueryFirstOrDefault<EbookEntity>(@"SELECT Document.Id,
                                                                               Document.IdEditor,
                                                                               Document.IdCollection,
                                                                               Document.Doi,
                                                                               Document.FileDocument,
                                                                               Document.MarcRecordNumber,
                                                                               Document.OriginalTitle,
                                                                               Document.TitlesVariants,
                                                                               Document.Subtitle,
                                                                               Document.Foreword,
                                                                               Document.Keywords,
                                                                               Document.FileFormat,
                                                                               Document.CoverPage,
                                                                               Document.Url,
                                                                               Document.DocumentType,
                                                                               Document.OriginalLanguage,
                                                                               Document.LanguagesVariants,
                                                                               Document.Translator,
                                                                               Document.AccessType,
                                                                               Document.State,
                                                                               Document.Price,
                                                                               Document.PublicationDate,
                                                                               Document.Country,
                                                                               Document.PhysicalDescription,
                                                                               Document.AccompanyingMaterials,
                                                                               Document.AccompanyingMaterialsNb,
                                                                               Document.VolumeNb,
                                                                               Document.IsMultiVolume,
                                                                               Document.DocumentVolumeRefs,
                                                                               Document.MarcFile,
                                                                               Document.BlankPages,
                                                                               Document.Abstract,
                                                                               Document.Notes,
                                                                               Document.Notes,
                                                                               Document.IdOriginal,
                                                                               Document.IsTranslated,
                                                                               Ebook.EditionNum,
                                                                               Ebook.EditionPlace,
                                                                               Ebook.ISBN,
                                                                               Ebook.Genre,
                                                                               Ebook.Category,
                                                                               Ebook.NbPages
                                                                        FROM Ebook
                                                                                 LEFT JOIN Document ON Ebook.Id = Document.Id
                                                                        WHERE Ebook.Id = @Id
                                                                          AND (Document.State = 'published' OR Document.State = 'unpublished')
                                                                        ORDER BY Ebook.Id DESC
                                                                        LIMIT 1", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int AddEbook(EbookEntity ebook)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" INSERT INTO Ebook (Id, EditionNum, EditionPlace, ISBN, Genre, Category, NbPages)
                                    VALUES (@Id, @EditionNum, @EditionPlace, @ISBN, @Genre, @Category, @NbPages);";

                    return connection.Execute(sql, ebook);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }
        public int UpdateEbook(EbookEntity ebook)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @" UPDATE Ebook
                                    SET EditionNum = @EditionNum,
                                        EditionPlace = @EditionPlace,
                                        ISBN = @ISBN,
                                        Genre = @Genre,
                                        Category = @Category,
                                        NbPages = @NbPages
                                    WHERE Id = @Id;";

                    return connection.Execute(sql, ebook);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public int DeleteEbook(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    string sql = @"DELETE FROM Ebook WHERE Id = @Id";

                    return connection.Execute(sql, new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} \n", ex);
                throw;
            }
        }

        public List<EbookEntity> GetRecentEbooks()
        {
            try
            {
                using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
                {
                    return connection.Query<EbookEntity>(@"SELECT Document.Id,
                                                                   Document.OriginalTitle,
                                                                   Document.Subtitle,
                                                                   Document.CoverPage,
                                                                   Document.Url
                                                            FROM Ebook
                                                                     LEFT JOIN Document ON Ebook.Id = Document.Id
                                                            WHERE (Document.State = 'published' OR Document.State = 'unpublished')
                                                            ORDER BY Ebook.Id DESC LIMIT 10;").ToList();
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

