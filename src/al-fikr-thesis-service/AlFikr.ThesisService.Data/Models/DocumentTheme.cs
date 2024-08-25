using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Documenttheme
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int IdTheme { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;

    public virtual Theme IdThemeNavigation { get; set; } = null!;
}
