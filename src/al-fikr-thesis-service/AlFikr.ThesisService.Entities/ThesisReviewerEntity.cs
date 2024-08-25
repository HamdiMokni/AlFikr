namespace AlFikr.ThesisService.Entities
{
	public class ThesisReviewerEntity
	{
		public int Id { get; set; }
		public int ThesisId { get; set; }
		public int? ReviewerId { get; set; }
		public string? Role { get; set; }

	}
}
