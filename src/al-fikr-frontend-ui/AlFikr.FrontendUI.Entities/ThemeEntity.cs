using System.Text.Json.Serialization;

namespace AlFikr.FrontendUI.Entities;

public class ThemeEntity
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("arTitle")]
    public string ArTitle { get; set; }

    [JsonPropertyName("shortTitle")]
    public string? ShortTitle { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
