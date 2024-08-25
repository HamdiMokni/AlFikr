using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Collection;

public class UpsertModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;

    [BindProperty]
    public CollectionEntity Collection { get; set; }

    public List<EditorEntity> Editors { get; set; } = new();

    public UpsertModel(IEbookServiceApiClient ebookServiceApiClient)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
    }

    public void OnGet(int id)
    {
        if (id != 0)
        {
            Collection = ebookServiceApiClient.GetCollectionAsync(id).Result;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Collection.IdEditor = 1;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await ebookServiceApiClient.UpsertCollectionAsync(Collection);
        }
        catch (ApiException ex)
        {
            return HandleError(ex, nameof(Theme));
        }
        catch (Exception ex)
        {
            return HandleError(ex.Message);
        }

        return RedirectToPage("Index");
    }
}
