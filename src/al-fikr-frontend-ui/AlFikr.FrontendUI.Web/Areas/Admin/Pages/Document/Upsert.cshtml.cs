using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Document
{
    public class UpsertModel : PageModel
    {

        private readonly IEbookServiceApiClient ebookServiceApiClient;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public EbookEntity Ebook { get; set; }
        public void OnGet(int id)
        {
            if (id != 0)
            {
                Ebook = ebookServiceApiClient.GetEbookAsync(id).Result;
            }
        }
        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    // Handle the case where form validation fails
            //    return Page();
            //}

            //if (Ebook.FileDocument == null || Ebook.FileDocument.Length == 0)
            //{

            //    return Page();
            //}
            //var folderPath = Path.Combine(_hostEnvironment.WebRootPath, "images\\books");
            //List<string> chunkPaths = await ebookServiceApiClient.SlicePdfIntoChunksAsync(Ebook.attachmentFileDocument, folderPath);
            //await ebookServiceApiClient.RegroupChunksIntoFileAsync(chunkPaths, folderPath);
            return Page();
        }
    }
}
