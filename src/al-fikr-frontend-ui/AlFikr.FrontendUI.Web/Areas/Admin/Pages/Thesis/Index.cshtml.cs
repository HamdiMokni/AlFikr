using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Entities.Exceptions;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Thesis
{
	public class IndexModel : PageModelBase
	{
		private readonly IThesisServiceApiClient thesisServiceApiClient;
		private readonly ILogger<IndexModel> _logger;
		public IEnumerable<ThesisEntity> ListOfThese { get; set; }

		public IndexModel(IThesisServiceApiClient thesisServiceApiClient, ILogger<IndexModel> logger)
		{
			this.thesisServiceApiClient = thesisServiceApiClient;
			_logger = logger;

		}

		public async Task OnGet()
		{
			ListOfThese = await thesisServiceApiClient.GetTheses();
		}

		public async Task<IActionResult> OnPostDeleteAsync(int id)
		{
			try
			{
				await thesisServiceApiClient.RemoveThesisAsync(id);
			}
			catch (ApiException ex)
			{
				_logger.LogError(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return RedirectToPage("Index");
		}
	}
}
