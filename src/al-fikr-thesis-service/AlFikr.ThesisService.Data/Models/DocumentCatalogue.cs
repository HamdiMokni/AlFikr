using System;
using System.Collections.Generic;

namespace AlFikr.ThesisService.Data.Models;

public partial class Documentcatalogue
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int IdCatalogue { get; set; }

    public virtual Catalogue IdCatalogueNavigation { get; set; } = null!;

    public virtual Document IdDocumentNavigation { get; set; } = null!;
}
