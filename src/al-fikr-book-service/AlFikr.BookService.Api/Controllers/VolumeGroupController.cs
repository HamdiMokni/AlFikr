using AlFikr.BookService.Business;
using AlFikr.BookService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.BookService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolumeGroupController : BaseController<VolumeGroupController>
    {
        private readonly VolumeGroupService _volumeGroupService;

        public VolumeGroupController(VolumeGroupService authorService, ILogger<VolumeGroupController> logger) : base(logger)
        {
            _volumeGroupService = authorService;
        }

        [HttpGet]
        public IEnumerable<VolumeGroupEntity> GetVolumeGroups()
        {
            return _volumeGroupService.GetVolumeGroups();
        }

        [HttpGet("{id}")]
        public VolumeGroupEntity GetVolumeGroup(int id)
        {
            return _volumeGroupService.GetVolumeGroup(id);
        }

        [HttpPost]
        public IActionResult InsertVolumeGroup([FromBody] VolumeGroupEntity volumeGroup)
        {
            if (volumeGroup == null)
                return BadRequest(new { errorMessage = $"{nameof(volumeGroup)} can not be null!" });

            var affectedRows = _volumeGroupService.AddVolumeGroup(volumeGroup);

            if (affectedRows > 0)
                return Created();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Insert Volume Group");
        }

        [HttpPut]
        public IActionResult UpdateVolumeGroup([FromBody] VolumeGroupEntity author)
        {
            if (author == null)
                return BadRequest(new { errorMessage = $"{nameof(author)} can not be null!" });

            var affectedRows = _volumeGroupService.UpdateVolumeGroup(author);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, "Unable to Update Volume Group");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVolumeGroup(int id)
        {
            if (id == 0)
                return BadRequest(new { errorMessage = $"{nameof(id)} can not be null!" });

            var affectedRows = _volumeGroupService.DeleteVolumeGroup(id);

            if (affectedRows > 0)
                return NoContent();

            return StatusCode((int)HttpStatusCode.NotFound, $"Unable to Remove Volume Group");
        }
    }
}
