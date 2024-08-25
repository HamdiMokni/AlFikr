using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Documentfile
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public string Reference { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? FileDocument { get; set; }

    public string? FileFormat { get; set; }

    public int StartPage { get; set; }

    public int EndPage { get; set; }

    public string State { get; set; } = null!;

    public DateTime? AddedOn { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;
}
