using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Thesis
{
    public int Id { get; set; }

    public string? Speciality { get; set; }

    public string? Discipline { get; set; }

    public string? Institution { get; set; }

    public string? University { get; set; }

    public string? Type { get; set; }

    public DateTime? DefenceDate { get; set; }

    public string? Supervisor { get; set; }

    public string? Reviewer { get; set; }

    public int? NbPages { get; set; }

    public virtual Document IdNavigation { get; set; } = null!;

    public virtual ICollection<Thesisreviewer> Thesisreviewers { get; set; } = new List<Thesisreviewer>();

    public virtual ICollection<Thesissupervisor> Thesissupervisors { get; set; } = new List<Thesissupervisor>();
}
