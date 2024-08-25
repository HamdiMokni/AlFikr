using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public int IdEditor { get; set; }
        public int IdCollection { get; set; }
        public string Doi { get; set; }
        public string MarcRecordNumber { get; set; }
        public string OriginalTitle { get; set; }
        public string TitlesVariants { get; set; }
        public string Subtitle { get; set; }
        public string Foreword { get; set; }
        public string Keywords { get; set; }
        public string FileDocument { get; set; }
        public string FileFormat { get; set; }
        public string CoverPage { get; set; }
        public string Url { get; set; }
        public string DocumentType { get; set; }
        public string OriginalLanguage { get; set; }
        public string LanguagesVariants { get; set; }
        public string Translator { get; set; }
        public string AccessType { get; set; }
        public string Author { get; set; }
        public string State { get; set; }
        public float Price { get; set; }
        public string PublicationDate { get; set; }
        public string Country { get; set; }
        public string PhysicalDescription { get; set; }
        public string AccompanyingMaterials { get; set; }
        public string AccompanyingMaterialsNb { get; set; }
        public string VolumeNb { get; set; }
        public string Abstract { get; set; }
        public string Notes { get; set; }
        public int BlankPages { get; set; }
        public float SellingPrice { get; set; }
        public float DigitalPrice { get; set; }
        public bool Downloadable { get; set; }
        public string MarcFile { get; set; }
        public bool IsMultiVolume { get; set; }
        public int DocumentVolumeRefs { get; set; }
        public int IdOriginal { get; set; }
        public bool IsTranslated { get; set; }
    }
}
