namespace AlFikr.ThesisService.Data.Models;

public partial class Thesisreviewer
{
	public int Id { get; set; }

	public int ThesisId { get; set; }

	public int ReviewerId { get; set; }

	public string? Role { get; set; }

	public virtual Reviewer? Reviewer { get; set; }

	public virtual Thesis? Thesis { get; set; }
}
