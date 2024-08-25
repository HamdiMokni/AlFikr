using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Ebook
{
    public int Id { get; set; }

    public string? EditionNum { get; set; }

    public string? EditionPlace { get; set; }

    public string? Isbn { get; set; }

    public string? Genre { get; set; }

    public string? Category { get; set; }

    public int? NbPages { get; set; }

    public virtual Document IdNavigation { get; set; } = null!;
}
