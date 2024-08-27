using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Translationgroup
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int? IdGroupRefs { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;

    public virtual Translationgroup? IdGroupRefsNavigation { get; set; }

    public virtual ICollection<Translationgroup> InverseIdGroupRefsNavigation { get; set; } = new List<Translationgroup>();
}
