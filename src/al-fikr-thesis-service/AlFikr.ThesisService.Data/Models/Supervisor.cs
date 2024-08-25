using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Supervisor
{
    public int Id { get; set; }

    public string? SupervisorName { get; set; }

    public string? SupervisorArName { get; set; }

    public string? SupervisorTitle { get; set; }

    public DateTime? AddedOn { get; set; }

    public virtual ICollection<Thesissupervisor> Thesissupervisors { get; set; } = new List<Thesissupervisor>();
}
