using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class VolumeGroup
{
    public int Id { get; set; }

    public int? IdDocument { get; set; }

    public int? NumVolume { get; set; }

    public int? IdGroupRefs { get; set; }

    public virtual Document? IdDocumentNavigation { get; set; }
}
