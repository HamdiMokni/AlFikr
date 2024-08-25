using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class TranslationGroup
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int? IdGroupRefs { get; set; }

    public virtual Document IdDocumentNavigation { get; set; } = null!;

    public virtual TranslationGroup? IdGroupRefsNavigation { get; set; }

    public virtual ICollection<TranslationGroup> InverseIdGroupRefsNavigation { get; set; } = new List<TranslationGroup>();
}
