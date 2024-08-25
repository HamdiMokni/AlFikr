using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Author
{
    public int Id { get; set; }

    public string? OrcId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? ArName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Civility { get; set; }

    public string? City { get; set; }

    public string? Adress { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Position { get; set; }

    public string? Email { get; set; }

    public string? Biography { get; set; }

    public string? Phone { get; set; }

    public string? Photo { get; set; }

    public int? IdEditor { get; set; }

    public virtual ICollection<Documentauthor> Documentauthors { get; set; } = new List<Documentauthor>();

    public virtual Editor? IdEditorNavigation { get; set; }
}
