using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.ThesisService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentFilesController : BaseController<DocumentFilesController>
	{
		private readonly DocumentFilesService _documentFilesService;

		public DocumentFilesController(DocumentFilesService documentFilesService, ILogger<DocumentFilesController> logger) : base(logger)
		{
			_documentFilesService = documentFilesService;
		}

		[HttpGet]
		public IEnumerable<DocumentFilesEntity> GetDocumentFiles()
		{
			return _documentFilesService.GetDocumentFiles();
		}

		[HttpGet("{id}")]
		public DocumentFilesEntity GetDocumentFile(int id)
		{
			return _documentFilesService.GetDocumentFile(id);
		}
		[HttpPost]
		public IActionResult InsertDocumentFiles([FromBody] DocumentFilesEntity documentFiles)
		{
			if (documentFiles == null)
				return BadRequest(new { errorMessage = $"{nameof(documentFiles)} can not be null!" });

			var affectedRows = _documentFilesService.AddDocumentFile(documentFiles);

			if (affectedRows > 0)
				return Created();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Previews");
		}
		[HttpPut]
		public IActionResult UpdateDocumentFile([FromBody] DocumentFilesEntity documentFiles)
		{
			if (documentFiles == null)
				return BadRequest(new { errorMessage = $"{nameof(documentFiles)} can not be null!" });

			var affectedRows = _documentFilesService.UpdateDocumentFile(documentFiles);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Preview");
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteDocumentFile(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			var affectedRows = _documentFilesService.DeleteDocumentFiles(id);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Preview");
		}
	}
}
