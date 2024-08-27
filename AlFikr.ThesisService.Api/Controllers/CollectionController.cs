using AlFikr.ThesisService.Business;
using AlFikr.ThesisService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.ThesisService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CollectionController : BaseController<CollectionController>
	{
		private readonly CollectionService _collectionService;

		public CollectionController(CollectionService collectionService, ILogger<CollectionController> logger) : base(logger)
		{
			_collectionService = collectionService;
		}

		[HttpGet]
		public IEnumerable<CollectionEntity> GetCollections()
		{
			return _collectionService.GetCollections();
		}
		[HttpGet("{id}")]
		public CollectionEntity GetCollection(int id)
		{
			return _collectionService.GetCollection(id);
		}

		[HttpPost]
		public IActionResult InsertCollection(CollectionEntity collection)
		{
			if (collection == null)
				return BadRequest(new { errorMessage = $"{nameof(collection)} can not be null!" });

			var affectedRows = _collectionService.AddCollection(collection);

			if (affectedRows > 0)
				return Created();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Collection");
		}

		[HttpPut]
		public IActionResult UpdateCollection(CollectionEntity collection)
		{
			if (collection == null)
				return BadRequest(new { errorMessage = $"{nameof(collection)} can not be null!" });

			var affectedRows = _collectionService.UpdateCollection(collection);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Collection");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteCollection(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			var affectedRows = _collectionService.DeleteCollection(id);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove collection");
		}
	}
}
