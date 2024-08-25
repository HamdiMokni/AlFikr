using System.Text.Json.Serialization;

namespace AlFikr.FrontendUI.Entities
{
	public class ThesisSupervisorEntity
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("thesisId")]
		public int ThesisId { get; set; }
		[JsonPropertyName("supervisorId")]
		public int SupervisorId { get; set; }
	}
}
