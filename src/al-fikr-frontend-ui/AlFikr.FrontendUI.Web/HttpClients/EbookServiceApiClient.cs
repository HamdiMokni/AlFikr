using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.Extensions;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Text;
using System.Text.Json;

namespace AlFikr.FrontendUI.Web.HttpClients;

public class EbookServiceApiClient: ClientBase,IEbookServiceApiClient
{
    private readonly HttpClient _httpClient;

    public EbookServiceApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
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

        if(InvalidStatusCode(result)) 
            ThrowError("Error retrieving all authors",  result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<List<AuthorEntity>>();
    }
  
    public async Task RemoveAuthorAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Author/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when removing the author: {errorMessage}", response);
        }
    }

    public async Task UpsertAuthorAsync(AuthorEntity author, IFormFile authorPhoto, IWebHostEnvironment hostEnvironment)
    {
        var content = CreateJsonContent(author);
        HttpResponseMessage response = new HttpResponseMessage();

        if(author.Id == null)
            response = await _httpClient.PostAsync("Author", content);
        else
            response = await _httpClient.PutAsync("Author", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when manipulating the author: {errorMessage}", response);
        }

        if(response.IsSuccessStatusCode)
        {
            var res = response.Content.ReadAsStringAsync().Result;

            if (author.Id != null)
                res = author.Id.ToString();

            try
            {
                if (authorPhoto != null)
                {
                    var PhotoFileName = $"{res}_{Guid.NewGuid().ToString().Substring(0, 4)}.jpg";

                    var uploads = Path.Combine(hostEnvironment.WebRootPath, "images\\authors");

                    var filePath = Path.Combine(uploads, PhotoFileName);

                    authorPhoto.CopyTo(new FileStream(filePath, FileMode.Create));

                    author.Id = int.Parse(res);
                    author.Photo = PhotoFileName;

                    content = CreateJsonContent(author);

                    response = await _httpClient.PutAsync("Author", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ThrowError($"Error inserting author: {errorMessage}", response);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
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
            ThrowError("Error retrieving theme", result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<ThemeEntity>();
    }

    public async Task UpsertThemeAsync(ThemeEntity theme)
    {
        var content = CreateJsonContent(theme);

        HttpResponseMessage response = new();

        if (theme.Id == null)
            response = await _httpClient.PostAsync("Theme", content);
        else
            response = await _httpClient.PutAsync("Theme", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when manipulating the theme: {errorMessage}", response);
        }
    }

    public async Task RemoveThemeAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Theme/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when removing the theme: {errorMessage}", response);
        }
    }

    // Collection API Calls
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

    public async Task<CollectionEntity> GetCollectionAsync(int id)
    {
        var result = await _httpClient.GetAsync($"Collection/{id}");

        if (InvalidStatusCode(result))
            ThrowError("Error retrieving collection", result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<CollectionEntity>();
    }

    public async Task UpsertCollectionAsync(CollectionEntity collection)
    {
        var content = CreateJsonContent(collection);

        HttpResponseMessage response = new();

        if (collection.Id == null)
            response = await _httpClient.PostAsync("Collection", content);
        else
            response = await _httpClient.PutAsync("Collection", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when manipulating the collection: {errorMessage}", response);
        }
    }

    public async Task RemoveCollectionAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Collection/{id}");

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            ThrowError($"Error occured when removing the collection: {errorMessage}", response);
        }
    }
    //Ebook 
    public async Task<List<EbookEntity>> GetEbooks()
    {
        var result = await _httpClient.GetAsync("Ebook");

        if (InvalidStatusCode(result))
            ThrowError("Error retrieving all Ebooks", result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<List<EbookEntity>>();
    }
    public async Task<EbookEntity> GetEbookAsync(int id)
    {
        var result = await _httpClient.GetAsync($"Ebook/{id}");

        if (InvalidStatusCode(result))
            ThrowError("Error retrieving Ebook", result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<EbookEntity>();
    }

    public async Task<List<EbookEntity>> GetRecentEbooksAsync()
    {
        var result = await _httpClient.GetAsync("Ebook/RecentEbooks");

        if (InvalidStatusCode(result))
            ThrowError("Error retrieving recent Ebooks", result);

        var content = await result.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
            return null;

        return content.ToEntity<List<EbookEntity>>();
    }
}
