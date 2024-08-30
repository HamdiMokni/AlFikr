using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Viewer
{
    public class PdfViewerModel : PageModel
    {
        public string PdfFilePath { get; private set; }

        public IActionResult OnGet(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
            {
                pdfPath = "images/Thesis/48.pdf"; // Default PDF path
            }

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pdfPath);
            if (!System.IO.File.Exists(fullPath))
            {
                pdfPath = "images/Thesis/48.pdf"; // Fallback to default PDF
                fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pdfPath);

                if (!System.IO.File.Exists(fullPath))
                {
                    return NotFound();
                }
            }

            PdfFilePath = $"/{pdfPath}";
            return Page();
        }
    }
}
