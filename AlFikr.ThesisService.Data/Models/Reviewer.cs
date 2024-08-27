using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Reviewer
{
    public int Id { get; set; }

    public string? ReviewerName { get; set; }

    public string? ReviewerArName { get; set; }

    public int IdEditor { get; set; }

    public DateTime? AddedOn { get; set; }

    public virtual Editor IdEditorNavigation { get; set; } = null!;

    public virtual ICollection<Thesisreviewer> Thesisreviewers { get; set; } = new List<Thesisreviewer>();
}
