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
	public class SupervisorController : BaseController<SupervisorController>
	{
		private readonly SupervisorService _SupervisorService;
		private readonly IMapper _mapper;
		private readonly ThesisSupervisorServices _thesisSupervisorService;

		public SupervisorController(SupervisorService supervisorService, ThesisSupervisorServices thesisSupervisorService, IMapper mapper, ILogger<SupervisorController> logger) : base(logger)
		{
			_SupervisorService = supervisorService;
			_mapper = mapper;
			_thesisSupervisorService = thesisSupervisorService;

		}

		[HttpGet]
		public IEnumerable<SupervisorEntity> GetSupervisors()
		{
			return _SupervisorService.GetSupervisors();
		}

		[HttpGet("{id}")]
		public SupervisorEntity GetSupervisor(int id)
		{
			return _SupervisorService.GetSupervisor(id);
		}

		[HttpGet("supervisor Theses/{supervisorId}")]
		public IActionResult GetThesesBySupervisor(int supervisorId)
		{
			var theses = _SupervisorService.GetThesesBySupervisorId(supervisorId);
			if (theses == null || theses.Count == 0)
			{
				return NotFound();
			}
			return Ok(theses);
		}

		[HttpPost]
		public IActionResult InsertSupervisor([FromBody] SupervisorEntity SupervisorEntity)
		{
			if (SupervisorEntity == null)
				return BadRequest(new { errorMessage = $"{nameof(SupervisorEntity)} can not be null!" });

			return RunAndHandleError(() =>
			{
				Supervisor Supervisor = _mapper.Map<Supervisor>(SupervisorEntity);

				int result = _SupervisorService.AddSupervisor(SupervisorEntity);
				if (result == 0)
				{
					return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to add supervisor");
				}

				return Created();
			}, "Error when adding new Supervisor");
		}

		[HttpPut]
		public IActionResult UpdateSupervisor([FromBody] SupervisorEntity SupervisorEntity)
		{
			if (SupervisorEntity == null)
				return BadRequest("thesis Supervisor Entity should not be empty!");


			return RunAndHandleError(() =>
			{
				Supervisor Supervisor = _mapper.Map<Supervisor>(SupervisorEntity);

				int result = _SupervisorService.UpdateSupervisor(SupervisorEntity);
				if (result == 0)
				{
					return NotFound();
				}

				return Created();
			}, "Unable to Update Supervisor");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteSupervisor(int id)
		{
			return RunAndHandleError(() =>
			{
				int rowsAffected = _thesisSupervisorService.RemoveSupervisorsBySupervisorId(id);
				if (rowsAffected == 0)
				{
					return NotFound(new { errorMessage = "Supervisor not found in thesisSupervisor table or could not be removed" });
				}

				int result = _SupervisorService.DeleteSupervisor(id);
				if (result == 0)
				{
					return NotFound(new { errorMessage = "Supervisor not found in Supervisor table or could not be deleted" });
				}

				return NoContent();
			}, "Error when deleting Supervisor");
		}
	}
}
