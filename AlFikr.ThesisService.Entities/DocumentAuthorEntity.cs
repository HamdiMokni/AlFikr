using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class DocumentAuthorEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }
	[JsonPropertyName("idAuthor")]
	public int? IdAuthor { get; set; }
	[JsonPropertyName("idDocument")]
	public int? IdDocument { get; set; }
	[JsonPropertyName("role")]
	public string? Role { get; set; }
}
