using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Theme
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? ArTitle { get; set; }

    public string? ShortTitle { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Documenttheme> Documentthemes { get; set; } = new List<Documenttheme>();

    public virtual ICollection<Subtheme> Subthemes { get; set; } = new List<Subtheme>();
}
