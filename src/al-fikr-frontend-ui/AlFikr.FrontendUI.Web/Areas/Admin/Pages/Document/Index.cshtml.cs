using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Document
{
    public class IndexModel : PageModel
    {
        private readonly IEbookServiceApiClient ebookServiceApiClient;
  
        public IEnumerable<EbookEntity> ListOfEbooks { get; set; }

        public IndexModel(IEbookServiceApiClient ebookServiceApiClient)
        {
            this.ebookServiceApiClient = ebookServiceApiClient;
            
        }
        public void OnGet()
        {
            ListOfEbooks = ebookServiceApiClient.GetEbooks().Result;
        }
    }
}
