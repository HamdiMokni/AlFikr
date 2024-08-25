namespace AlFikr.ThesisService.Data.Models;

public partial class Thesissupervisor
{
	public int Id { get; set; }

	public int ThesisId { get; set; }

	public int SupervisorId { get; set; }

	public string? Role { get; set; }

	public virtual Supervisor? Supervisor { get; set; }

	public virtual Thesis? Thesis { get; set; }
}
