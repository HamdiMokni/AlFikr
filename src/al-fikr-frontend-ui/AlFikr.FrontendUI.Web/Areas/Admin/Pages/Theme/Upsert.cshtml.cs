using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Theme;

public class UpsertModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;

    [BindProperty]
    public ThemeEntity Theme { get; set; }

    public UpsertModel(IEbookServiceApiClient ebookServiceApiClient)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
    }

    public void OnGet(int id)
    {
        if (id != 0)
        {
            Theme = ebookServiceApiClient.GetThemeAsync(id).Result;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await ebookServiceApiClient.UpsertThemeAsync(Theme);
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
