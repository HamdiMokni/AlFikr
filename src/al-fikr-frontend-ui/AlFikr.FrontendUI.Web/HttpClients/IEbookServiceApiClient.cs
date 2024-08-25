using AlFikr.FrontendUI.Entities;

namespace AlFikr.FrontendUI.Web.HttpClients;

public interface IEbookServiceApiClient
{
    Task<List<AuthorEntity>> GetAuthorsAsync();
    Task<AuthorEntity> GetAuthorAsync(int id);
    Task UpsertAuthorAsync(AuthorEntity author, IFormFile authorPhoto, IWebHostEnvironment hostEnvironment);
    Task RemoveAuthorAsync(int id);

    Task<List<ThemeEntity>> GetThemesAsync();
    Task<ThemeEntity> GetThemeAsync(int id);
    Task UpsertThemeAsync(ThemeEntity theme);
    Task RemoveThemeAsync(int id);
    Task<List<CollectionEntity>> GetCollectionsAsync();
    Task<CollectionEntity> GetCollectionAsync(int id);
    Task UpsertCollectionAsync(CollectionEntity collection);
    Task RemoveCollectionAsync(int id);
    Task<List<EbookEntity>> GetEbooks();
    Task<EbookEntity> GetEbookAsync(int id);
    Task<List<EbookEntity>> GetRecentEbooksAsync();
}
