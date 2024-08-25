using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class AuthorEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }
	[JsonPropertyName("idEditor")]
	public int? IdEditor { get; set; }
	[JsonPropertyName("Theses")]
	public List<ThesisEntity>? Theses { get; set; }
	[JsonPropertyName("orcId")]
	public string? OrcId { get; set; }
	[Required(ErrorMessage = "FirstName is required")]
	[JsonPropertyName("firstName")]
	public string FirstName { get; set; }
	[Required(ErrorMessage = "LastName is required")]
	[JsonPropertyName("lastName")]
	public string LastName { get; set; }
	[Required(ErrorMessage = "ArName is required")]
	[JsonPropertyName("arName")]
	public string ArName { get; set; }
	[JsonPropertyName("dateOfBirth")]
	public DateTime? DateOfBirth { get; set; }
	[JsonPropertyName("civility")]
	public string? Civility { get; set; }
	[JsonPropertyName("city")]
	public string? City { get; set; }
	[JsonPropertyName("adress")]
	public string? Adress { get; set; }
	[JsonPropertyName("postalCode")]
	public string? PostalCode { get; set; }
	[JsonPropertyName("country")]
	public string? Country { get; set; }
	[JsonPropertyName("position")]
	public string? Position { get; set; }
	[JsonPropertyName("email")]
	public string? Email { get; set; }
	[JsonPropertyName("biography")]
	public string? Biography { get; set; }
	[JsonPropertyName("phone")]
	public string? Phone { get; set; }
	[JsonPropertyName("photo")]
	public string? Photo { get; set; }
}