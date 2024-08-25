using System.Text.Json.Serialization;

namespace AlFikr.FrontendUI.Entities
{
	public partial class ReviewerEntity
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("reviewerName")]
		public string reviewerName { get; set; }
		[JsonPropertyName("reviewerArName")]
		public string reviewerArName { get; set; }
		[JsonPropertyName("addedOn")]
		public DateTime AddedOn { get; set; }
	}
}