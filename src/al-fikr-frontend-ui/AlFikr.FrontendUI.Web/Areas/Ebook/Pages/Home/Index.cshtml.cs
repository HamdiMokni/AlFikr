using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AlFikr.FrontendUI.Web.Areas.Ebook.Pages.Home;

public class IndexModel : PageModelBase
{
	private readonly IEbookServiceApiClient ebookServiceApiClient;
	public List<ThemeEntity> Themes { get; set; }
	public List<EbookEntity> RecentEbooks { get; set; }

	public IndexModel(IEbookServiceApiClient ebookServiceApiClient)
	{
		this.ebookServiceApiClient = ebookServiceApiClient;
	}

	public void OnGet()
	{
		//Themes = ebookServiceApiClient.GetThemesAsync().Result;
		//RecentEbooks = ebookServiceApiClient.GetRecentEbooksAsync().Result;
	}

	public IActionResult OnPostCultureManagement(string culture, string returnUrl)
	{
		try
		{
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
			   new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
		}
		catch (ApiException ex)
		{
			return HandleError(ex, nameof(culture));
		}
		catch (Exception ex)
		{
			return HandleError(ex.Message);
		}

		return LocalRedirect(returnUrl);
	}

	public IActionResult OnPostAdvancedSearch(AdvancedSearchEntity advancedSearchEntity)
	{
		try
		{

		}
		catch (ApiException ex)
		{
			return HandleError(ex, nameof(advancedSearchEntity));
		}
		catch (Exception ex)
		{
			return HandleError(ex.Message);
		}

		return Page();
	}
}
