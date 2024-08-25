using AlFikr.BookService.Business;
using AlFikr.BookService.Data.Models;
using AlFikr.BookService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AlFikr.BookService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EbookController : BaseController<EbookController>
    {
        private readonly EbookService _ebookService;
        private readonly AlFikrContext _alFikrContext;
        private readonly IMapper _mapper;

        public EbookController(EbookService ebookService, AlFikrContext alFikrContext, IMapper mapper, ILogger<EbookController> logger) : base(logger)
        {
            _ebookService = ebookService;
            _alFikrContext = alFikrContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<EbookEntity> GetEbooks()
        {
            return _ebookService.GetEbooks();
        }

        [HttpGet("{id}")]
        public EbookEntity GetEbook(int id)
        {
            return _ebookService.GetEbook(id);
        }

        [HttpGet("RecentEbooks")]
        public List<EbookEntity> GetRecentEbooks()
        {
            return _ebookService.GetRecentEbooks(); ;
        }

        [HttpPost]
        public IActionResult InsertEbook([FromBody] EbookEntity ebookEntity)
        {
            if (ebookEntity == null)
                return BadRequest(new { errorMessage = $"{nameof(ebookEntity)} can not be null!" });

            using var trans = _alFikrContext.Database.BeginTransaction();

            return RunAndHandleError(() =>
            {
                Document document = _mapper.Map<Document>(ebookEntity);

                _alFikrContext.Documents.Add(document);
                _alFikrContext.SaveChanges();

                Ebook ebook = _mapper.Map<Ebook>(ebookEntity);
                ebook.Id = document.Id;

                _alFikrContext.Ebooks.Add(ebook);
                _alFikrContext.SaveChanges();
                
                trans.Commit();

                return Created();
            }, "Error when adding new Ebook", trans);
        }

        [HttpPut]
        public IActionResult UpdateEbook([FromBody] EbookEntity ebookEntity)
        {
            if (ebookEntity == null)
                return BadRequest("Ebook Entity should not be empty!");

            using var trans = _alFikrContext.Database.BeginTransaction();

            return RunAndHandleError(() =>
            {
                Document document = _mapper.Map<Document>(ebookEntity);
                Ebook ebook = _mapper.Map<Ebook>(ebookEntity);

                _alFikrContext.Documents.Update(document);
                _alFikrContext.Ebooks.Update(ebook);

                _alFikrContext.SaveChanges();

                trans.Commit();

                return Created();
            }, "Unable to Update Ebook", trans);
        }
    }
}
