using AlFikr.FrontendUI.Entities;

namespace AlFikr.FrontendUI.Web.HttpClients;
public interface IThesisServiceApiClient
{
	/*   Editor   */
	Task<EditorEntity> GetEditorAsync(int id);

	/*   Author   */
	Task<List<AuthorEntity>> GetAuthorsAsync();
	Task<AuthorEntity> GetAuthorAsync(int id);
	/*   Theme   */
	Task<List<ThemeEntity>> GetThemesAsync();
	Task<ThemeEntity> GetThemeAsync(int id);
	/*   Catalogues   */
	Task<List<CatalogueEntity>> GetCataloguesAsync();
	/*   Collection   */
	Task<List<CollectionEntity>> GetCollectionsAsync();
	Task<CollectionEntity> GetCollectionAsync(int id);


	/*   Thesis   */
	Task<List<ThesisEntity>> GetTheses();
	Task<ThesisEntity> GetThesis(int Id);
	Task<HttpResponseMessage> UpsertThesisAsync(ThesisEntity thesis);
	Task RemoveThesisAsync(int id);

	/*   Supervisor   */
	Task<HttpResponseMessage> UpsertSupervisorAsync(SupervisorEntity supervisor);
	Task<List<SupervisorEntity>> GetSupervisorsAsync();
	/*   Reviewer   */
	Task<List<ReviewerEntity>> GetReviewersAsync();
	Task<ReviewerEntity> GetReviewerAsync(int id);
    Task<List<ThesisEntity>> AdvancedSearchThesesAsync(List<AdvancedSearchItem> criteria);


}

