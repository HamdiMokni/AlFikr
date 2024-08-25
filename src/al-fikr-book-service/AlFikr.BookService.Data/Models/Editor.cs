using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class Editor
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ArName { get; set; }

    public string? WebSite { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? About { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public int? Multiplyer { get; set; }

    public string? PhotoFileName { get; set; }

    public string? Status { get; set; }

    public DateTime? AddedOn { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
