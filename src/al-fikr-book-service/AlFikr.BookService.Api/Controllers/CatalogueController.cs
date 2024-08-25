using AlFikr.BookService.Business;
using AlFikr.BookService.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.BookService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogueController : BaseController<CatalogueController>
    {
        private readonly CatalogueService _catalogueService;

        public CatalogueController(CatalogueService catalogueService, ILogger<CatalogueController> logger) : base(logger)
        {
            _catalogueService = catalogueService;
        }

        [HttpGet]
        public IEnumerable<CatalogueEntity> GetCatalogues()
        {
            return _catalogueService.GetCatalogues();
        }

        [HttpGet("{id}")]
        public CatalogueEntity GetCatalogue(int id)
        {
            return _catalogueService.GetCatalogue(id);
        }

        [HttpPost]
        public IActionResult InsertCatalogue([FromBody] CatalogueEntity catalogue)
        {
            if (catalogue == null)
                return BadRequest(new { errorMessage = $"{nameof(catalogue)} can not be null!" });

            var affectedRows = _catalogueService.AddCatalogue(catalogue);

            if (affectedRows > 0)
                return Created();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Catalogue");
        }

        [HttpPut]
        public IActionResult UpdateCatalogue([FromBody] CatalogueEntity catalogue)
        {
            if (catalogue == null)
                return BadRequest(new { errorMessage = $"{nameof(catalogue)} can not be null!" });

            var affectedRows = _catalogueService.UpdateCatalogue(catalogue);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Catalogue");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCatalogue(int id)
        {
            if (id == 0)
                return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

            var affectedRows = _catalogueService.DeleteCatalogue(id);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Catalogue");
        }
    }
}
