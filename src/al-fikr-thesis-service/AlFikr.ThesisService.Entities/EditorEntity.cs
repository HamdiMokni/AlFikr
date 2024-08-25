using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class EditorEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }

	[Required(ErrorMessage = "Editor name is required")]
	[JsonPropertyName("name")]
	public string Name { get; set; }

	[Required(ErrorMessage = "Editor arabic name is required")]
	[JsonPropertyName("arName")]
	public string ArName { get; set; }

	[JsonPropertyName("webSite")]
	public string? WebSite { get; set; }

	[JsonPropertyName("email")]
	public string? Email { get; set; }

	[JsonPropertyName("phone")]
	public string? Phone { get; set; }

	[JsonPropertyName("about")]
	public string? About { get; set; }

	[JsonPropertyName("city")]
	public string? City { get; set; }

	[JsonPropertyName("country")]
	public string? Country { get; set; }

	[JsonPropertyName("address")]
	public string? Address { get; set; }

	[JsonPropertyName("postalCode")]
	public string? PostalCode { get; set; }

	[JsonPropertyName("multiplyer")]
	public int? Multiplyer { get; set; }

	[JsonPropertyName("photo")]
	public string? Photo { get; set; }

	[JsonPropertyName("status")]
	public string? Status { get; set; }

	[JsonPropertyName("addedOn")]
	public DateTime? AddedOn { get; set; }
	[JsonPropertyName("mainUser")]
	public UserEntity? MainUser { get; set; }
}
