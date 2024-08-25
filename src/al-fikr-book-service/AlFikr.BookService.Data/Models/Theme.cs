using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class Theme
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? ArTitle { get; set; }

    public string? ShortTitle { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<SubTheme> SubThemes { get; set; } = new List<SubTheme>();
}
