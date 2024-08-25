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
	public class ReviewerController : BaseController<ReviewerController>
	{
		private readonly ReviewerService _ReviewerService;
		private readonly IMapper _mapper;
		private readonly ThesisReviewerServices _thesisReviewerService;

		public ReviewerController(ReviewerService ReviewerService, ThesisReviewerServices thesisReviewerService, IMapper mapper, ILogger<ReviewerController> logger) : base(logger)
		{
			_ReviewerService = ReviewerService;
			_mapper = mapper;
			_thesisReviewerService = thesisReviewerService;

		}

		[HttpGet]
		public IEnumerable<ReviewerEntity> GetReviewers()
		{
			return _ReviewerService.GetReviewers();
		}

		[HttpGet("{id}")]
		public ReviewerEntity GetReviewer(int id)
		{
			return _ReviewerService.GetReviewer(id);
		}

		[HttpGet("Reviewer Theses/{ReviewerId}")]
		public IActionResult GetThesesByReviewer(int ReviewerId)
		{
			var theses = _ReviewerService.GetThesesByReviewerId(ReviewerId);
			if (theses == null || theses.Count == 0)
			{
				return NotFound();
			}
			return Ok(theses);
		}

		[HttpPost]
		public IActionResult InsertReviewer([FromBody] ReviewerEntity ReviewerEntity)
		{
			if (ReviewerEntity == null)
				return BadRequest(new { errorMessage = $"{nameof(ReviewerEntity)} can not be null!" });

			return RunAndHandleError(() =>
			{
				Reviewer Reviewer = _mapper.Map<Reviewer>(ReviewerEntity);

				int result = _ReviewerService.AddReviewer(ReviewerEntity);
				if (result == 0)
				{
					return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to add Reviewer");
				}

				return Created();
			}, "Error when adding new Reviewer");
		}

		[HttpPut]
		public IActionResult UpdateReviewer([FromBody] ReviewerEntity ReviewerEntity)
		{
			if (ReviewerEntity == null)
				return BadRequest("thesis Reviewer Entity should not be empty!");


			return RunAndHandleError(() =>
			{
				Reviewer Reviewer = _mapper.Map<Reviewer>(ReviewerEntity);

				int result = _ReviewerService.UpdateReviewer(ReviewerEntity);
				if (result == 0)
				{
					return NotFound();
				}

				return Created();
			}, "Unable to Update Reviewer");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteReviewer(int id)
		{
			return RunAndHandleError(() =>
			{
				int rowsAffected = _thesisReviewerService.RemoveReviewersByReviewerId(id);
				if (rowsAffected == 0)
				{
					return NotFound(new { errorMessage = "Reviewer not found in thesisReviewer table or could not be removed" });
				}

				int result = _ReviewerService.DeleteReviewer(id);
				if (result == 0)
				{
					return NotFound(new { errorMessage = "Reviewer not found in Reviewer table or could not be deleted" });
				}

				return NoContent();
			}, "Error when deleting Reviewer");
		}
	}
}
