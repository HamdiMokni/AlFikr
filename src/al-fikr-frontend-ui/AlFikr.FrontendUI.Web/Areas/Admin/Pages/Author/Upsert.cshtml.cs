using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Author;

public class UpsertModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;
    private readonly IWebHostEnvironment _hostEnvironment;

    [BindProperty]
    public AuthorEntity Author {  get; set; }
    [BindProperty]
    public IFormFile? AuthorPhoto {  get; set; }

    public UpsertModel(IEbookServiceApiClient ebookServiceApiClient, IWebHostEnvironment hostEnvironment)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
        this._hostEnvironment = hostEnvironment;
    }

    public void OnGet(int id)
    {
        if(id != 0)
        {
            Author = ebookServiceApiClient.GetAuthorAsync(id).Result;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await ebookServiceApiClient.UpsertAuthorAsync(Author, AuthorPhoto, _hostEnvironment);
        }
        catch(ApiException ex)
        {
            return HandleError(ex, nameof(Author));
        }
        catch(Exception ex)
        {
            return HandleError(ex.Message);
        }

        return RedirectToPage("Index");
    }
}
