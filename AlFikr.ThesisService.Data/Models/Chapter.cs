using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Chapter
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? IdDocument { get; set; }

    public int? ChapterNumber { get; set; }

    public int? StartPage { get; set; }

    public int? StartPageOuv { get; set; }

    public int? EndPage { get; set; }

    public string? Description { get; set; }

    public string? State { get; set; }

    public virtual Document? IdDocumentNavigation { get; set; }
}
