using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AlFikr.BookService.Entities;

public class SubThemeEntity
{
    public int Id { get; set; }

    public int IdTheme { get; set; }

    public int IdCollection { get; set; }

    public string Title { get; set; }

    public string ArTitle { get; set; }

    public string ShortTitle { get; set; }

    public string Description { get; set; }
}
