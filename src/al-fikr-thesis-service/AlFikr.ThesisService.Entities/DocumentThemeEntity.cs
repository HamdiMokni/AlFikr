using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class DocumentThemeEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }
	[JsonPropertyName("idTheme")]
	public int? IdTheme { get; set; }
	[JsonPropertyName("idDocument")]
	public int? IdDocument { get; set; }
}
