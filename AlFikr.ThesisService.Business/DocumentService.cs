using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace AlFikr.ThesisService.Business
{
	public class DocumentService : ServiceBase<DocumentService>
	{
		private readonly IConfiguration configuration;

		public DocumentService(AlFikrContext alFikrContext, ILogger<DocumentService> logger, IConfiguration configuration) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
		}
		public IEnumerable<DocumentEntity> GetDocuments()
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.Query<DocumentEntity>("SELECT * FROM Document");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public DocumentEntity GetDocument(int id)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					return connection.QueryFirstOrDefault<DocumentEntity>("SELECT * FROM Document WHERE Id = @Id", new { Id = id });
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int AddDocument(DocumentEntity document)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" INSERT INTO Document(IdEditor,IdCollection,Doi,MarcRecordNumber,OriginalTitle,TitlesVariants,Subtitle,Foreword,Keywords,FileDocument,FileFormat,CoverPage,Url,DocumentType,OriginalLanguage,LanguagesVariants,Translator,AccessType,State,Price,SellingPrice,DigitalPrice,PublicationDate,Country,PhysicalDescription,AccompanyingMaterials,AccompanyingMaterialsNb,VolumeNb,DocumentVolumeRefs,IsMultiVolume,IdOriginal,IsTranslated,Abstract,Notes,BlankPages,Downloadable)
                                    VALUES(@IdEditor,@IdCollection,@Doi,@MarcRecordNumber,@OriginalTitle,@TitlesVariants,@Subtitle,@Foreword,@Keywords,@FileDocument,@FileFormat,@CoverPage,@Url,@DocumentType,@OriginalLanguage,@LanguagesVariants,@Translator,@AccessType,@State,@Price,@SellingPrice,@DigitalPrice,@PublicationDate,@Country,@PhysicalDescription,@AccompanyingMaterials,@AccompanyingMaterialsNb,@VolumeNb,@DocumentVolumeRefs,@IsMultiVolume,@IdOriginal,@IsTranslated,@Abstract,@Notes,@BlankPages,@Downloadable)";

					return connection.Execute(sql, document);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}
		public int UpdateDocument(DocumentEntity document)
		{
			try
			{
				using (var connection = new MySqlConnection(configuration.GetConnectionString("AlFikr")))
				{
					string sql = @" UPDATE Document 
                                    SET IdEditor=@IdEditor,
                                        IdCollection=@IdCollection,
                                        Doi=@Doi,
                                        MarcRecordNumber=@MarcRecordNumber,
                                        OriginalTitle=@OriginalTitle,
                                        TitlesVariants=@TitlesVariants,
                                        Subtitle=@Subtitle,
                                        Foreword=@Foreword,
                                        Keywords=@Keywords,
                                        FileDocument=@FileDocument,
                                        FileFormat=@FileFormat,
                                        CoverPage=@CoverPage,
                                        Url=@Url,
                                        DocumentType=@DocumentType,
                                        OriginalLanguage=@OriginalLanguage,
                                        LanguagesVariants=@LanguagesVariants,
                                        Translator=@Translator,
                                        AccessType=@AccessType,
                                        State=@State,
                                        Price=@Price,
                                        SellingPrice=@SellingPrice,
                                        DigitalPrice=@DigitalPrice,
                                        PublicationDate=@PublicationDate,
                                        Country=@Country,
                                        PhysicalDescription=@PhysicalDescription,
                                        AccompanyingMaterials=@AccompanyingMaterials,
                                        AccompanyingMaterialsNb=@AccompanyingMaterialsNb,
                                        VolumeNb=@VolumeNb,
                                        IsMultiVolume=@IsMultiVolume,
                                        IdOriginal=@IdOriginal,
                                        IsTranslated=@IsTranslated,
                                        DocumentVolumeRefs=@DocumentVolumeRefs,
                                        Abstract=@Abstract,
                                        Notes=@Notes,
                                        BlankPages=@BlankPages,
                                        Downloadable=@Downloadable,
                                        MarcFile=@MarcFile
                                    WHERE Id=@Id";

					return connection.Execute(sql, document);
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
