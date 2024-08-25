using AlFikr.BookService.Business;
using AlFikr.BookService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlFikr.BookService.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthorController : BaseController<AuthorController>
	{
		private readonly AuthorService _authorService;

		public AuthorController(AuthorService authorService, ILogger<AuthorController> logger) : base(logger)
		{
			_authorService = authorService;
		}

		[HttpGet]
		public IEnumerable<AuthorEntity> GetAuthors()
		{
			return _authorService.GetAuthors();
		}

		[HttpGet("{id}")]
		public AuthorEntity GetAuthor(int id)
		{
			return _authorService.GetAuthor(id);
		}

		[HttpPost]
		public IActionResult InsertAuthor(AuthorEntity author)
		{
			if (author == null)
				return BadRequest(new { errorMessage = $"{nameof(author)} can not be null!" });

			var authorId = _authorService.AddAuthor(author);

			if (authorId > 0)
				return Ok(authorId);

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Author");
		}

		[HttpPut]
		public IActionResult UpdateAuthor(AuthorEntity author)
		{
			if (author == null)
				return BadRequest(new { errorMessage = $"{nameof(author)} can not be null!" });

			var affectedRows = _authorService.UpdateAuthor(author);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Author");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteAuthor(int id)
		{
			if (id == 0)
				return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

			var affectedRows = _authorService.DeleteAuthor(id);

			if (affectedRows > 0)
				return NoContent();

			return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Author");
		}
	}
}
