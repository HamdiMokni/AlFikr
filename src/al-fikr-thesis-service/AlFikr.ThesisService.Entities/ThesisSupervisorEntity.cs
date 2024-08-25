namespace AlFikr.ThesisService.Entities
{
	public class ThesisSupervisorEntity
	{
		public int Id { get; set; }
		public int ThesisId { get; set; }
		public int? SupervisorId { get; set; }
		public string? Role { get; set; }

	}
}
