using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Author;

public class IndexModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;

    public List<AuthorEntity> Authors { get; set; }

    public IndexModel(IEbookServiceApiClient ebookServiceApiClient)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
    }

    public void OnGet()
    {
        Authors = ebookServiceApiClient.GetAuthorsAsync().Result;
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await ebookServiceApiClient.RemoveAuthorAsync(id);
        }
        catch (ApiException ex)
        {
            return HandleError(ex, nameof(id));
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }

        return RedirectToPage("Index");
    }
}
