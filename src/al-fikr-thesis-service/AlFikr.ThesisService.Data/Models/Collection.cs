using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Collection
{
    public int Id { get; set; }

    public int IdEditor { get; set; }

    public string Title { get; set; } = null!;

    public string ArTitle { get; set; } = null!;

    public string ShortTitle { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual Editor IdEditorNavigation { get; set; } = null!;

    public virtual ICollection<Subtheme> Subthemes { get; set; } = new List<Subtheme>();
}
