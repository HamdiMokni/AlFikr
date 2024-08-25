using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities;

public class UserEntity
{
	[JsonPropertyName("id")]
	public int? Id { get; set; }
	[JsonPropertyName("idAffiliation")]
	public int? IdAffiliation { get; set; }
	[JsonPropertyName("roleInAffiliation")]
	public string? RoleInAffiliation { get; set; }
	[JsonPropertyName("accountType")]
	public string AccountType { get; set; }
	[JsonPropertyName("code")]
	public string? Code { get; set; }
	[JsonPropertyName("email")]
	public string? Email { get; set; }
	[JsonPropertyName("password")]
	public string? Password { get; set; }
	[JsonPropertyName("emailConfirmed")]
	public bool? EmailConfirmed { get; set; }
	[JsonPropertyName("emailConfirmationCode")]
	public string? EmailConfirmationCode { get; set; }
	[JsonPropertyName("codeExpirationDate")]
	public DateTime? CodeExpirationDate { get; set; }
	[JsonPropertyName("status")]
	public string? Status { get; set; }
	[JsonPropertyName("photo")]
	public string? Photo { get; set; }
	[JsonPropertyName("addedOn")]
	public DateTime? AddedOn { get; set; }
}
