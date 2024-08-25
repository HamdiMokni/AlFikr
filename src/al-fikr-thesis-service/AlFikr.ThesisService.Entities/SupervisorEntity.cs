using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities
{
	public class SupervisorEntity
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("supervisorName")]
		public string? SupervisorName { get; set; }
		[JsonPropertyName("supervisorArName")]
		public string? SupervisorArName { get; set; }
		[JsonPropertyName("supervisorRole")]
		public string? SupervisorRole { get; set; }
		[JsonPropertyName("supervisorTitle")]
		public string? SupervisorTitle { get; set; }
		[JsonPropertyName("addedOn")]
		public DateTime? AddedOn { get; set; }
	}
}
