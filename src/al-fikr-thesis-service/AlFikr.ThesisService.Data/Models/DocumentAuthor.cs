using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Documentauthor
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int IdAuthor { get; set; }

    public string Role { get; set; } = null!;

    public virtual Author IdAuthorNavigation { get; set; } = null!;

    public virtual Document IdDocumentNavigation { get; set; } = null!;
}
