using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class CollectionEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[JsonPropertyName("idEditor")]
	public int? IdEditor { get; set; }

	[JsonPropertyName("title")]
	[Required(ErrorMessage = "Collection Title is required")]
	public string Title { get; set; }

	[JsonPropertyName("arTitle")]
	[Required(ErrorMessage = "Collection Arabic Title is required")]
	public string ArTitle { get; set; }

	[JsonPropertyName("shortTitle")]
	[Required(ErrorMessage = "Collection Short title is required")]
	public string ShortTitle { get; set; }

	[JsonPropertyName("description")]
	public string? Description { get; set; }
}

