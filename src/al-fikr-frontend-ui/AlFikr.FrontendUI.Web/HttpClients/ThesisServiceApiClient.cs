using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.Extensions;
using Newtonsoft.Json;

namespace AlFikr.FrontendUI.Web.HttpClients;

public class ThesisServiceApiClient : ClientBase, IThesisServiceApiClient
{
	private readonly HttpClient _httpClient;

	public ThesisServiceApiClient(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}
	// Editor API Calls
	public async Task<EditorEntity> GetEditorAsync(int id)
	{
		var result = await _httpClient.GetAsync($"Editor/{id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving Editor", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<EditorEntity>();
	}

	// Author API Calls
	public async Task<AuthorEntity> GetAuthorAsync(int id)
	{
		var result = await _httpClient.GetAsync($"Author/{id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving author", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<AuthorEntity>();
	}
	public async Task<List<AuthorEntity>> GetAuthorsAsync()
	{
		var result = await _httpClient.GetAsync("Author");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all authors", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<AuthorEntity>>();
	}

	// Catalgoue API Calls
	public async Task<List<CatalogueEntity>> GetCataloguesAsync()
	{
		var result = await _httpClient.GetAsync("Catalogue");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all Catalogues", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<CatalogueEntity>>();
	}

	// Collection API Calls
	public async Task<CollectionEntity> GetCollectionAsync(int id)
	{
		var result = await _httpClient.GetAsync($"Collection/{id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving Collection", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<CollectionEntity>();
	}

	public async Task<List<CollectionEntity>> GetCollectionsAsync()
	{
		var result = await _httpClient.GetAsync("Collection");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all collections", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<CollectionEntity>>();
	}
	// Supervisor API Calls
	public async Task<HttpResponseMessage> UpsertSupervisorAsync(SupervisorEntity supervisor)
	{
		var content = CreateJsonContent(supervisor);
		HttpResponseMessage response = new();

		if (supervisor.Id == 0)
			response = await _httpClient.PostAsync("Supervisor", content);
		else
			response = await _httpClient.PutAsync("Supervisor", content);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ThrowError($"Error occured when manipulating the Supervisor: {errorMessage}", response);
		}

		return response;
	}
	public async Task<List<SupervisorEntity>> GetSupervisorsAsync()
	{
		var result = await _httpClient.GetAsync("Supervisor");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all Supervisors", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<SupervisorEntity>>();
	}
	// Reviewer API Calls
	public async Task<List<ReviewerEntity>> GetReviewersAsync()
	{
		var result = await _httpClient.GetAsync("Reviewer");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all Reviewers", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<ReviewerEntity>>();
	}

	public async Task<ReviewerEntity> GetReviewerAsync(int id)
	{
		var result = await _httpClient.GetAsync($"Reviewer/{id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving Reviewer", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<ReviewerEntity>();
	}

	// Theme API Calls
	public async Task<List<ThemeEntity>> GetThemesAsync()
	{
		var result = await _httpClient.GetAsync("Theme");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all themes", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<ThemeEntity>>();
	}

	public async Task<ThemeEntity> GetThemeAsync(int id)
	{
		var result = await _httpClient.GetAsync($"Theme/{id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving Theme", result);

		var content = await result.Content.ReadAsStringAsync();


		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<ThemeEntity>(); ;
	}


	// Thesis API Calls
	public async Task<List<ThesisEntity>> GetTheses()
	{
		var result = await _httpClient.GetAsync("Thesis");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving all Theses", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;

		return content.ToEntity<List<ThesisEntity>>();
	}
	public async Task<ThesisEntity> GetThesis(int Id)
	{
		var result = await _httpClient.GetAsync($"Thesis/{Id}");

		if (InvalidStatusCode(result))
			ThrowError("Error retrieving Thesis", result);

		var content = await result.Content.ReadAsStringAsync();

		if (string.IsNullOrEmpty(content))
			return null;
		ThesisEntity thesis = JsonConvert.DeserializeObject<ThesisEntity>(content);
		return thesis;
	}
	public async Task<HttpResponseMessage> UpsertThesisAsync(ThesisEntity thesis)
	{
		var content = CreateJsonContent(thesis);
		HttpResponseMessage response = new();

		if (thesis.Id == 0)
			response = await _httpClient.PostAsync("Thesis", content);
		else
			response = await _httpClient.PutAsync("Thesis", content);

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ThrowError($"Error occured when manipulating the thesis: {errorMessage}", response);
		}

		return response;
	}
	public async Task RemoveThesisAsync(int id)
	{
		var response = await _httpClient.DeleteAsync($"Thesis/{id}");

		if (!response.IsSuccessStatusCode)
		{
			var errorMessage = await response.Content.ReadAsStringAsync();
			ThrowError($"Error occured when removing the Thesis: {errorMessage}", response);
		}
	}

    public async Task<List<ThesisEntity>> AdvancedSearchThesesAsync(List<AdvancedSearchItem> criteria)
    {
        var response = await _httpClient.PostAsJsonAsync("Thesis/advanced-search", criteria);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ThesisEntity>>(responseContent);
    }

}

