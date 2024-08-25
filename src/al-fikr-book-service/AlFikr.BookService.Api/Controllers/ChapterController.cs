using AlFikr.BookService.Business;
using AlFikr.BookService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.BookService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : BaseController<ChapterController>
    {
        private readonly ChapterService _chapterService;

        public ChapterController(ChapterService chapterService, ILogger<ChapterController> logger) : base(logger)
        {
            _chapterService = chapterService;
        }

        [HttpGet]
        public IEnumerable<ChapterEntity> GetChapters()
        {
            return _chapterService.GetChapters();
        }

        [HttpGet("{id}")]
        public ChapterEntity GetChapter(int id)
        {
            return _chapterService.GetChapter(id);
        }
        [HttpPost]
        public IActionResult InsertChapter([FromBody] ChapterEntity chapter)
        {
            if (chapter == null)
                return BadRequest(new { errorMessage = $"{nameof(chapter)} can not be null!" });

            var affectedRows = _chapterService.AddChapter(chapter);

            if (affectedRows > 0)
                return Created();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Chapter");
        }

        [HttpPut]
        public IActionResult UpdateChapter([FromBody] ChapterEntity chapter)
        {
            if (chapter == null)
                return BadRequest(new { errorMessage = $"{nameof(chapter)} can not be null!" });

            var affectedRows = _chapterService.UpdateChapter(chapter);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Chapter");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteChapter(int id)
        {
            if (id == 0)
                return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

            var affectedRows = _chapterService.DeleteChapter(id);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Chapter");
        }
    }
}
