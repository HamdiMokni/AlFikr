using AlFikr.BookService.Business;
using AlFikr.BookService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlFikr.BookService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslationGroupController : BaseController<TranslationGroupController>
    {
        private readonly TranslationGroupService _TranslationGroupService;
        
        public TranslationGroupController(TranslationGroupService TranslationGroupService, ILogger<TranslationGroupController> logger) : base(logger)
        {
            _TranslationGroupService = TranslationGroupService;
        }

        [HttpGet]
        public IEnumerable<TranslationGroupEntity> GetTranslationGroups()
        {
            return _TranslationGroupService.GetTranslationGroups();
        }

        [HttpGet("{id}")]
        public TranslationGroupEntity GetTranslationGroup(int id)
        {
            return _TranslationGroupService.GetTranslationGroup(id);
        }

        [HttpPost]
        public IActionResult InsertTranslationGroup([FromBody] TranslationGroupEntity translation)
        {
            if (translation == null)
                return BadRequest(new { errorMessage = $"{nameof(translation)} can not be null!" });

            var affectedRows = _TranslationGroupService.AddTranslationGroup(translation);

            if(affectedRows > 0)
                return Created();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Translation Group");
        }

        [HttpPut]
        public IActionResult UpdateTranslationGroup([FromBody] TranslationGroupEntity translation)
        {
            if (translation == null)
                return BadRequest(new { errorMessage = $"{nameof(translation)} can not be null!" });

            var affectedRows = _TranslationGroupService.UpdateTranslationGroup(translation);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Translation Group");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTranslationGroup(int id)
        {
            if(id == 0)
                return BadRequest(new { errorMessage = $"{nameof(id)} can't be null!" });
            
            var affectedRows = _TranslationGroupService.DeleteTranslationGroup(id);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Translation Group");
        }
    }
}
