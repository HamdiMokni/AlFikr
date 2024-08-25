using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public int IdAffiliation { get; set; }

    public string RoleInAffiliation { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string EmailConfirmationCode { get; set; } = null!;

    public DateTime CodeExpirationDate { get; set; }

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? AddedOn { get; set; }
}
