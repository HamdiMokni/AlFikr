using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Collection;

public class IndexModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;

    public List<CollectionEntity> Collections { get; set; }

    public IndexModel(IEbookServiceApiClient ebookServiceApiClient)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
    }

    public void OnGet()
    {
        Collections = ebookServiceApiClient.GetCollectionsAsync().Result;
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await ebookServiceApiClient.RemoveCollectionAsync(id);
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
