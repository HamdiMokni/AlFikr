using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Individual
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ArName { get; set; }

    public string? Orcid { get; set; }

    public string? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public string? PostalCode { get; set; }

    public string? PhotoFileName { get; set; }

    public string? Profession { get; set; }

    public string? Organization { get; set; }
}
