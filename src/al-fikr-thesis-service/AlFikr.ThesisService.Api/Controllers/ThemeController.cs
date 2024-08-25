using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlFikr.ThesisService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ThemeController : BaseController<ThemeController>
	{
		private readonly ThemeService _themeService;

		public ThemeController(ThemeService themeService, ILogger<ThemeController> logger) : base(logger)
		{
			_themeService = themeService;
		}

		[HttpGet]
		public IEnumerable<ThemeEntity> GetThemes()
		{
			return _themeService.GetThemes();
		}

		[HttpGet("{id}")]
		public ThemeEntity GetTheme(int id)
		{
			return _themeService.GetTheme(id);
		}

		[HttpPost]
		public IActionResult InsertTheme(ThemeEntity theme)
		{
			if (theme == null)
				return BadRequest(new { errorMessage = $"{nameof(theme)} can not be null!" });

			var affectedRows = _themeService.AddTheme(theme);

			if (affectedRows > 0)
				return Created();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Theme");
		}

		[HttpPut]
		public IActionResult UpdateTheme(ThemeEntity theme)
		{
			if (theme == null)
				return BadRequest(new { errorMessage = $"{nameof(theme)} can not be null!" });

			var affectedRows = _themeService.UpdateTheme(theme);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Theme");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteTheme(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			var affectedRows = _themeService.DeleteTheme(id);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Theme");
		}
	}
}
