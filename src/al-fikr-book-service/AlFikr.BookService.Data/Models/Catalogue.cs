using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class Catalogue
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ArTitle { get; set; } = null!;

    public int IdOwner { get; set; }

    public string? OwnerName { get; set; }

    public string? OwnerType { get; set; }

    public string? ShortTitle { get; set; }

    public string? Description { get; set; }
}
