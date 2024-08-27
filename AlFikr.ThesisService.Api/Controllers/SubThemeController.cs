using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace AlFikr.ThesisService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SubThemeController : BaseController<SubThemeController>
	{
		private readonly SubThemeService _subThemeService;

		public SubThemeController(SubThemeService subThemeService, ILogger<SubThemeController> logger) : base(logger)
		{
			_subThemeService = subThemeService;
		}

		[HttpGet]
		public IEnumerable<SubThemeEntity> GetSubThemes()
		{
			return _subThemeService.GetSubThemes();
		}

		[HttpGet("{id}")]
		public SubThemeEntity GetSubTheme(int id)
		{
			return _subThemeService.GetSubTheme(id);
		}

		[HttpPost]
		public IActionResult InsertSubTheme([FromBody] SubThemeEntity subTheme)
		{
			if (subTheme == null)
				return BadRequest(new { errorMessage = $"{nameof(subTheme)} can not be null!" });

			var affectedRows = _subThemeService.AddSubTheme(subTheme);

			if (affectedRows > 0)
				return Created();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert SubTheme");
		}

		[HttpPut]
		public IActionResult UpdateSubTheme([FromBody] SubThemeEntity subTheme)
		{
			if (subTheme == null)
				return BadRequest(new { errorMessage = $"{nameof(subTheme)} can not be null!" });

			var affectedRows = _subThemeService.UpdateSubTheme(subTheme);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update SubTheme");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteSubTheme(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			var affectedRows = _subThemeService.DeleteSubTheme(id);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove SubTheme");
		}
	}
}
