using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Subtheme
{
    public int Id { get; set; }

    public int IdTheme { get; set; }

    public int? IdCollection { get; set; }

    public string? Title { get; set; }

    public string? ArTitle { get; set; }

    public string? ShortTitle { get; set; }

    public string? Description { get; set; }

    public virtual Collection? IdCollectionNavigation { get; set; }

    public virtual Theme IdThemeNavigation { get; set; } = null!;
}
