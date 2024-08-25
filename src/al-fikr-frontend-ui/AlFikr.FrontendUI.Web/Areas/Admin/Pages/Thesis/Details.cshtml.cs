using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.HttpClients;

namespace AlFikr.FrontendUI.Web.Areas.Admin.Pages.Thesis
{
	public class DetailsModel : PageModelBase
	{
		private readonly IThesisServiceApiClient thesisServiceApiClient;
		public ThesisEntity Thesis { get; set; }
		public AuthorEntity Author { get; set; }
		public EditorEntity Editor { get; set; }
		public List<ThemeEntity> Themes { get; set; } = new List<ThemeEntity>();
		public ThemeEntity Theme { get; set; }
		public List<DocumentFilesEntity> ListOfDocumentFiles { get; set; } = new List<DocumentFilesEntity> { };


		public CollectionEntity Collection { get; set; }

		public ReviewerEntity Reviewer { get; set; }

		public DetailsModel(IThesisServiceApiClient thesisServiceApiClient)
		{
			this.thesisServiceApiClient = thesisServiceApiClient;

		}
		public void OnGet(int id)
		{
			Thesis = thesisServiceApiClient.GetThesis(id).Result;

			Author = thesisServiceApiClient.GetAuthorAsync(int.Parse(Thesis.MainAuthorsIds[0])).Result;

			Editor = thesisServiceApiClient.GetEditorAsync(Thesis.IdEditor).Result;

			foreach (string ThemesId in Thesis.ThemesIds)
			{
				Themes.Add(thesisServiceApiClient.GetThemeAsync(int.Parse(ThemesId)).Result);
			}

			Collection = thesisServiceApiClient.GetCollectionAsync(Thesis.IdCollection).Result;

			Reviewer = thesisServiceApiClient.GetReviewerAsync(Thesis.ReviewerIds[0].Key).Result;
		}
	}
}
