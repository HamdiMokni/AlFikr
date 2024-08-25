using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities
{
	public class ThesisEntity : DocumentEntity
	{

		[JsonPropertyName("speciality")]
		public string? Speciality { get; set; }
		[JsonPropertyName("discipline")]
		public string? Discipline { get; set; }
		[JsonPropertyName("institution")]
		public string? Institution { get; set; }
		[JsonPropertyName("university")]
		public string? University { get; set; }
		[JsonPropertyName("type")]
		public string? Type { get; set; }
		[JsonPropertyName("defenceDate")]
		public DateTime? DefenceDate { get; set; }
		[JsonPropertyName("nbPages")]
		public int? NbPages { get; set; }
		[JsonPropertyName("supervisorsIds")]
		public KeyValuePair<int, string>[] SupervisorsIds { get; set; } = new KeyValuePair<int, string>[0];

		[JsonPropertyName("supervisorList")]
		public List<SupervisorEntity> SupervisorList { get; set; } = new List<SupervisorEntity>();

		[JsonPropertyName("reviewerIds")]
		public List<KeyValuePair<int, string>> ReviewerIds { get; set; }
	}
}
