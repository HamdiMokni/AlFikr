using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Theme;

public class IndexModel : PageModelBase
{
    private readonly IEbookServiceApiClient ebookServiceApiClient;

    public List<ThemeEntity> Themes { get; set; }

    public IndexModel(IEbookServiceApiClient ebookServiceApiClient)
    {
        this.ebookServiceApiClient = ebookServiceApiClient;
    }

    public void OnGet()
    {
        Themes = ebookServiceApiClient.GetThemesAsync().Result;
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        try
        {
            await ebookServiceApiClient.RemoveThemeAsync(id);
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
