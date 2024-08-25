using System;
using System.Collections.Generic;

namespace AlFikr.BookService.Data.Models;

public partial class Document
{
    public int Id { get; set; }

    public int? IdEditor { get; set; }

    public int? IdCollection { get; set; }

    public string? Doi { get; set; }

    public string? MarcRecordNumber { get; set; }

    public string? OriginalTitle { get; set; }

    public string? TitlesVariants { get; set; }

    public string? Subtitle { get; set; }

    public string? Foreword { get; set; }

    public string? Keywords { get; set; }

    public string? FileDocument { get; set; }

    public string? FileFormat { get; set; }

    public string? CoverPage { get; set; }

    public string? Url { get; set; }

    public string? DocumentType { get; set; }

    public string? OriginalLanguage { get; set; }

    public string? LanguagesVariants { get; set; }

    public string? Translator { get; set; }

    public string? AccessType { get; set; }

    public string? Author { get; set; }

    public string? State { get; set; }

    public float? Price { get; set; }

    public string? PublicationDate { get; set; }

    public string? Country { get; set; }

    public string? PhysicalDescription { get; set; }

    public string? AccompanyingMaterials { get; set; }

    public string? AccompanyingMaterialsNb { get; set; }

    public string? VolumeNb { get; set; }

    public string? Abstract { get; set; }

    public string? Notes { get; set; }

    public string? BlankPages { get; set; }

    public float? SellingPrice { get; set; }

    public float? DigitalPrice { get; set; }

    public bool? Downloadable { get; set; }

    public string? MarcFile { get; set; }

    public bool? IsMultiVolume { get; set; }

    public int? DocumentVolumeRefs { get; set; }

    public int? IdOriginal { get; set; }

    public bool? IsTranslated { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<DocumentFile> DocumentFiles { get; set; } = new List<DocumentFile>();

    public virtual Ebook? Ebook { get; set; }

    public virtual Collection? IdCollectionNavigation { get; set; }

    public virtual Editor? IdEditorNavigation { get; set; }

    public virtual TranslationGroup? TranslationGroup { get; set; }

    public virtual ICollection<VolumeGroup> VolumeGroups { get; set; } = new List<VolumeGroup>();
}
