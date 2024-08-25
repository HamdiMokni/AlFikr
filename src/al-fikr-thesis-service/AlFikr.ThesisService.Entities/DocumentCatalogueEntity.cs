using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class DocumentCatalogueEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }
	[JsonPropertyName("idCatalogue")]
	public int? IdCatalogue { get; set; }
	[JsonPropertyName("idDocument")]
	public int? IdDocument { get; set; }
}
