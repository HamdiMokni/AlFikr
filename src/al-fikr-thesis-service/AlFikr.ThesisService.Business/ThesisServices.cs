using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore; // Add this using directive
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AlFikr.ThesisService.Business
{
	public class ThesisServices : ServiceBase<ThesisServices>
	{
		private readonly IConfiguration configuration;
		private readonly IMapper _mapper;

		public ThesisServices(AlFikrContext alFikrContext, ILogger<ThesisServices> logger, IConfiguration configuration, IMapper mapper) : base(alFikrContext, logger)
		{
			this.configuration = configuration;
			this._mapper = mapper;

		}
		public IEnumerable<ThesisEntity> GetTheses()
		{
			try
			{
				List<Thesis> theses = alFikrContext.Theses
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentauthors)
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentthemes)
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentcatalogues)
					.Include(x => x.Thesissupervisors)
						.ThenInclude(ts => ts.Supervisor)
					.Include(x => x.Thesisreviewers)
						.ThenInclude(ts => ts.Reviewer)
					.AsNoTracking()
					.ToList();

				if (theses == null || !theses.Any())
					return Enumerable.Empty<ThesisEntity>();

				return theses.Select(thesis => ToThesisEntity(thesis, _mapper)).ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public ThesisEntity? GetThesis(int id)
		{
			try
			{

				Thesis? thesis = alFikrContext.Theses
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentauthors)
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentthemes)
					.Include(x => x.IdNavigation)
						.ThenInclude(n => n.Documentcatalogues)
					.Include(x => x.Thesissupervisors)
						.ThenInclude(ts => ts.Supervisor)
					.Include(x => x.Thesisreviewers)
						.ThenInclude(ts => ts.Reviewer)
					.AsNoTracking()
					.FirstOrDefault(t => t.Id == id);

				if (thesis == null)
					return null;

				var tt = ToThesisEntity(thesis, _mapper);
				return tt;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message} \n", ex);
				throw;
			}
		}

		public static ThesisEntity ToThesisEntity(Thesis thesis, IMapper mapper)
		{
			ThesisEntity thesisEntity = new()
			{
				Id = thesis.Id,
				IdEditor = thesis.IdNavigation?.IdEditor,
				IdCollection = thesis.IdNavigation?.IdCollection,
				MainAuthorsIds = thesis.IdNavigation?.Documentauthors?.Where(x => x.Role.Equals("Author")).Select(x => x.IdAuthor.ToString()).ToArray() ?? Array.Empty<string>(),
				SecondaryAuthorsIds = thesis.IdNavigation?.Documentauthors?.Where(x => x.Role.Equals("Co-Author")).Select(x => x.IdAuthor.ToString()).ToArray() ?? Array.Empty<string>(),
				ThemesIds = thesis.IdNavigation?.Documentthemes?.Select(x => x.IdTheme.ToString()).ToArray() ?? Array.Empty<string>(),
				CataloguesIds = thesis.IdNavigation?.Documentcatalogues?.Select(x => x.IdCatalogue.ToString()).ToArray() ?? Array.Empty<string>(),
				SupervisorsIds = thesis.Thesissupervisors?.Select(x => new KeyValuePair<int, string>(x.SupervisorId, x.Role)).ToArray() ?? new KeyValuePair<int, string>[0],
				ReviewerIds = thesis.Thesisreviewers?.Select(x => new KeyValuePair<int, string>(x.ReviewerId, x.Role)).ToList() ?? new List<KeyValuePair<int, string>>(),
				Doi = thesis.IdNavigation?.Doi,
				MarcRecordNumber = thesis.IdNavigation?.MarcRecordNumber,
				OriginalTitle = thesis.IdNavigation?.OriginalTitle,
				TitlesVariants = thesis.IdNavigation?.TitlesVariants,
				Subtitle = thesis.IdNavigation?.Subtitle,
				Foreword = thesis.IdNavigation?.Foreword,
				Keywords = thesis.IdNavigation?.Keywords,
				FileDocument = thesis.IdNavigation?.FileDocument,
				FileFormat = thesis.IdNavigation?.FileFormat,
				CoverPage = thesis.IdNavigation?.CoverPage,
				Url = thesis.IdNavigation?.Url,
				DocumentType = thesis.IdNavigation?.DocumentType,
				OriginalLanguage = thesis.IdNavigation?.OriginalLanguage,
				LanguagesVariants = thesis.IdNavigation?.LanguagesVariants,
				Translator = thesis.IdNavigation?.Translator,
				AccessType = thesis.IdNavigation?.AccessType,
				State = thesis.IdNavigation?.State,
				PublicationDate = thesis.IdNavigation?.PublicationDate,
				Country = thesis.IdNavigation?.Country,
				PhysicalDescription = thesis.IdNavigation?.PhysicalDescription,
				AccompanyingMaterials = thesis.IdNavigation?.AccompanyingMaterials,
				AccompanyingMaterialsNb = thesis.IdNavigation?.AccompanyingMaterialsNb,
				Abstract = thesis.IdNavigation?.Abstract,
				Notes = thesis.IdNavigation?.Notes,
				BlankPages = thesis.IdNavigation?.BlankPages,
				Downloadable = thesis.IdNavigation?.Downloadable ?? false,
				MarcFile = thesis.IdNavigation?.MarcFile,
				IsMultiVolume = thesis.IdNavigation?.IsMultiVolume ?? false,
				DocumentVolumeRefs = thesis.IdNavigation?.DocumentVolumeRefs,
				IdOriginal = thesis.IdNavigation?.IdOriginal,
				IsTranslated = thesis.IdNavigation?.IsTranslated ?? false,
				Speciality = thesis.Speciality,
				Discipline = thesis.Discipline,
				Institution = thesis.Institution,
				University = thesis.University,
				Type = thesis.Type,
				DefenceDate = thesis.DefenceDate,
				NbPages = thesis.NbPages,
			};
			return thesisEntity;
		}
	}
}
