using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Data.Models;
using AlFikr.ThesisService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.ThesisService.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ThesisController : BaseController<ThesisController>
	{
		private readonly ThesisServices _thesisService;
		private readonly ThesisSupervisorServices _thesisSupervisorService;
		private readonly ThesisReviewerServices _thesisReviewerService;

		private readonly SupervisorService _SupervisorService;
		private readonly AlFikrContext _alFikrContext;
		private readonly IMapper _mapper;

		public ThesisController(ThesisServices thesisService, ThesisSupervisorServices thesisSupervisorServices, ThesisReviewerServices thesisReviewerServices, SupervisorService supervisorService, AlFikrContext alFikrContext, IMapper mapper, ILogger<ThesisController> logger) : base(logger)
		{
			_thesisService = thesisService;
			_thesisSupervisorService = thesisSupervisorServices;
			_thesisReviewerService = thesisReviewerServices;
			_SupervisorService = supervisorService;
			_alFikrContext = alFikrContext;
			_mapper = mapper;
		}
		[HttpGet]
		public IEnumerable<ThesisEntity> GetTheses()
		{
			return _thesisService.GetTheses();
		}

		[HttpGet("{id}")]
		public ThesisEntity GetThesis(int id)
		{

			return _thesisService.GetThesis(id);
		}

        [HttpPost("advanced-search")]
        public IActionResult AdvancedSearch([FromBody] List<AdvancedSearchItem> criteria)
        {
            _logger.LogInformation("Advanced search request received with criteria: {@Criteria}", criteria);

            if (criteria == null || !criteria.Any())
                return BadRequest("Search criteria cannot be null or empty.");

            var thesis = _thesisService.AdvancedSearch(criteria);

            if (thesis == null || !thesis.Any())
                return NotFound("No documents found matching the search criteria.");

            return Ok(thesis);
        }

        [HttpPost]
		public IActionResult InsertThesis(ThesisEntity thesisEntity)
		{
			if (thesisEntity == null)
				return BadRequest(new { errorMessage = $"{nameof(thesisEntity)} can not be null!" });

			using var trans = _alFikrContext.Database.BeginTransaction();

			return RunAndHandleError(() =>
			{
				Document document = _mapper.Map<Document>(thesisEntity);

				_alFikrContext.Documents.Add(document);
				_alFikrContext.SaveChanges();

				Thesis thesis = _mapper.Map<Thesis>(thesisEntity);
				thesis.Id = document.Id;

				if (thesisEntity.CataloguesIds?.Length > 0)
				{
					_alFikrContext.Documentcatalogues.AddRange(thesisEntity.CataloguesIds
						.Select(catalogueId => new Documentcatalogue { IdCatalogue = int.Parse(catalogueId), IdDocument = thesis.Id }));
				}

				if (thesisEntity.MainAuthorsIds?.Length > 0)
				{
					_alFikrContext.Documentauthors.AddRange(thesisEntity.MainAuthorsIds
						.Select(authorId => new Documentauthor { IdAuthor = int.Parse(authorId), IdDocument = thesis.Id, Role = "Author" }));
				}

				if (thesisEntity.SecondaryAuthorsIds?.Length > 0)
				{
					_alFikrContext.Documentauthors.AddRange(thesisEntity.SecondaryAuthorsIds
						.Select(authorId => new Documentauthor { IdAuthor = int.Parse(authorId), IdDocument = thesis.Id, Role = "Co-Author" }));
				}

				if (thesisEntity.ThemesIds?.Length > 0)
				{
					_alFikrContext.Documentthemes.AddRange(thesisEntity.ThemesIds
						.Select(themeId => new Documenttheme { IdTheme = int.Parse(themeId), IdDocument = thesis.Id }));
				}

				if (thesisEntity.SupervisorList.Count > 0)
				{
					foreach (var supervisor in thesisEntity.SupervisorList)
					{
						SupervisorEntity newSupervisor = new SupervisorEntity
						{
							Id = supervisor.Id,
							SupervisorName = supervisor.SupervisorName,
							SupervisorArName = supervisor.SupervisorArName,
							SupervisorTitle = supervisor.SupervisorTitle,
							AddedOn = DateTime.Now
						};

						int result = _SupervisorService.AddSupervisor(newSupervisor);
						if (result == 0)
						{
							return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to add supervisor");
						}
						Thesissupervisor thesisSupervisor = new Thesissupervisor
						{
							SupervisorId = result,
							ThesisId = thesis.Id,
							Role = supervisor.SupervisorRole
						};
						_alFikrContext.Thesissupervisors.Add(thesisSupervisor);
					}

				}

				if (thesisEntity.ReviewerIds.Count > 0)
				{
					_alFikrContext.Thesisreviewers.AddRange(thesisEntity.ReviewerIds
						.Select(kvp => new Thesisreviewer
						{
							ReviewerId = kvp.Key,
							ThesisId = thesis.Id,
							Role = kvp.Value
						})
					);
				}


				_alFikrContext.Theses.Add(thesis);

				_alFikrContext.SaveChanges();

				trans.Commit();

				// Return the ID of the created thesis

				return CreatedAtAction(nameof(InsertThesis), new { id = thesis.Id }, thesis.Id);
			}, "Error when adding new Thesis", trans);
		}

		[HttpPut]
		public IActionResult UpdateThesis([FromBody] ThesisEntity thesisEntity)
		{
			if (thesisEntity == null)
				return BadRequest("Thesis Entity should not be empty!");

			using var trans = _alFikrContext.Database.BeginTransaction();

			return RunAndHandleError(() =>
			{
				Thesis thesis = _mapper.Map<Thesis>(thesisEntity);

				_alFikrContext.Theses.Update(thesis);

				_alFikrContext.SaveChanges();

				trans.Commit();

				return Created();
			}, "Unable to Update Thesis", trans);
		}


		[HttpDelete("{id}")]
		public IActionResult DeleteThesis(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			using var trans = _alFikrContext.Database.BeginTransaction();

			return RunAndHandleError(() =>
			{
				Thesis thesis = _mapper.Map<Thesis>(_thesisService.GetThesis(id));
				Document document = _alFikrContext.Documents.SingleOrDefault(d => d.Id == id);

				if (thesis == null || document == null)
				{
					return NotFound(new { errorMessage = "Thesis not found" });

				}

				// Remove associated supervisors
				List<SupervisorEntity> listSupervisors = _thesisSupervisorService.GetSupervisorsByThesisId(id);
				_thesisSupervisorService.RemoveSupervisorsByThesisId(id);
				foreach (SupervisorEntity Supervisor in listSupervisors)
				{
					_SupervisorService.DeleteSupervisor(Supervisor.Id);
				}
				// Remove associated Reviewer
				_thesisReviewerService.RemoveReviewersByThesisId(id);

				// Remove Thesis
				_alFikrContext.Theses.Remove(thesis);
				_alFikrContext.SaveChanges();

				// Remove associated document
				_alFikrContext.Documents.Remove(document);
				_alFikrContext.SaveChanges();

				trans.Commit();

				return NoContent();
			}, "Error when deleting thesis", trans);
		}
	}
}
