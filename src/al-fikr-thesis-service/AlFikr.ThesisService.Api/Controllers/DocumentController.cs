using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.ThesisService.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class DocumentController : BaseController<DocumentController>
	{
		private readonly DocumentService _documentService;

		public DocumentController(DocumentService documentService, ILogger<DocumentController> logger) : base(logger)
		{
			_documentService = documentService;
		}

		[HttpGet]
		public IEnumerable<DocumentEntity> GetDocuments()
		{
			return _documentService.GetDocuments();
		}

		[HttpGet("{id}")]
		public DocumentEntity GetDocument(int id)
		{
			return _documentService.GetDocument(id);
		}

		[HttpPost]
		public IActionResult InsertDocument([FromBody] DocumentEntity document)
		{
			if (document == null)
				return BadRequest(new { errorMessage = $"{nameof(document)} can not be null!" });

			var affectedRows = _documentService.AddDocument(document);

			if (affectedRows > 0)
				return Created();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Document");
		}

		[HttpPut]
		public IActionResult UpdateDocument([FromBody] DocumentEntity document)
		{
			if (document == null)
				return BadRequest("DocumentEntity should not be empty!");

			var affectedRows = _documentService.UpdateDocument(document);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Document");
		}
	}
}
