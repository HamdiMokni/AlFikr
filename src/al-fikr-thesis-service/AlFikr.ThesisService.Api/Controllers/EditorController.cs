using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlFikr.ThesisService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EditorController : BaseController<EditorController>
{
	private readonly EditorService _editorService;

	public EditorController(EditorService editorService, EmailService emailService, ILogger<EditorController> logger) : base(logger)
	{
		_editorService = editorService;
	}

	[HttpGet]
	public IEnumerable<EditorEntity> GetEditors()
	{
		return _editorService.GetEditors();
	}

	[HttpGet("{id}")]
	public EditorEntity GetEditor(int id)
	{
		return _editorService.GetEditor(id);
	}

	[HttpPost]
	public IActionResult InsertEditor(EditorEntity editor)
	{
		if (editor == null)
			return BadRequest(new { errorMessage = $"{nameof(editor)} can not be null!" });

		return RunAndHandleError(() =>
		{
			var editorId = _editorService.AddEditor(editor);

			return Ok(editor.Id);

		}, "Unable to Insert Editor");
	}

	[HttpPut]
	public IActionResult UpdateEditor(EditorEntity editor)
	{
		if (editor == null)
			return BadRequest(new { errorMessage = $"{nameof(editor)} can not be null!" });

		return RunAndHandleError(() =>
		{
			var affectedRows = _editorService.UpdateEditor(editor);

			if (affectedRows > 0)
				return NoContent();

			return Ok(editor.Id);

		}, "Unable to Update Editor");
	}

	[HttpDelete("{id}")]
	public IActionResult DeleteEditor(int id)
	{
		if (id == 0)
			return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

		var affectedRows = _editorService.DeleteEditor(id);

		if (affectedRows > 0)
			return NoContent();

		return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Editor");
	}
}