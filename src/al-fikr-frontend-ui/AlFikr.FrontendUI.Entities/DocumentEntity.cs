using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlFikr.FrontendUI.Entities
{
	public class DocumentEntity
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		[JsonPropertyName("idEditor")]
		public int IdEditor { get; set; }
		[JsonPropertyName("idCollection")]
		public int IdCollection { get; set; }
		[JsonPropertyName("doi")]
		public string Doi { get; set; }
		[JsonPropertyName("marcRecordNumber")]
		public string MarcRecordNumber { get; set; }
		[JsonPropertyName("originalTitle")]
		public string OriginalTitle { get; set; }
		[JsonPropertyName("titlesVariants")]
		public string TitlesVariants { get; set; }
		[JsonPropertyName("subtitle")]
		public string Subtitle { get; set; }
		[JsonPropertyName("foreword")]
		public string? Foreword { get; set; }
		[JsonPropertyName("keywords")]
		public string? Keywords { get; set; }
		[JsonPropertyName("fileDocument")]
		public string FileDocument { get; set; }
		[JsonPropertyName("fileFormat")]
		public string FileFormat { get; set; }
		[JsonPropertyName("coverPage")]
		public string CoverPage { get; set; }
		[JsonPropertyName("url")]
		public string Url { get; set; }
		[JsonPropertyName("documentType")]
		public string DocumentType { get; set; }
		[JsonPropertyName("originalLanguage")]
		public string OriginalLanguage { get; set; }
		[JsonPropertyName("languagesVariants")]
		public string LanguagesVariants { get; set; }
		[JsonPropertyName("translator")]
		public string Translator { get; set; }
		[JsonPropertyName("accessType")]
		public string AccessType { get; set; }
		[JsonPropertyName("mainAuthorsIds")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string[]? MainAuthorsIds { get; set; }
		[JsonPropertyName("secondaryAuthorsIds")]
		public string[]? SecondaryAuthorsIds { get; set; }
		[JsonPropertyName("themesIds")]
		public string[]? ThemesIds { get; set; }
		[JsonPropertyName("cataloguesIds")]
		public string[]? CataloguesIds { get; set; }
		[JsonPropertyName("author")]
		public string Author { get; set; }
		[JsonPropertyName("state")]
		public string State { get; set; }
		[JsonPropertyName("price")]
		public float Price { get; set; }
		[JsonPropertyName("pubicationDate")]
		public string? PublicationDate { get; set; }
		[JsonPropertyName("country")]
		public string Country { get; set; }
		[JsonPropertyName("physicalDescription")]
		public string PhysicalDescription { get; set; }
		[JsonPropertyName("accompanyingMaterials")]
		public string AccompanyingMaterials { get; set; }
		[JsonPropertyName("accompanyingMaterialsNb")]
		public string AccompanyingMaterialsNb { get; set; }
		[JsonPropertyName("volumeNb")]
		public string VolumeNb { get; set; }
		[JsonPropertyName("abstract")]
		public string? Abstract { get; set; }
		[JsonPropertyName("notes")]
		public string Notes { get; set; }
		[JsonPropertyName("blankPages")]
		public int BlankPages { get; set; }
		[JsonPropertyName("sellingPrice")]
		public float SellingPrice { get; set; }
		[JsonPropertyName("digitalPrice")]
		public float DigitalPrice { get; set; }
		[JsonPropertyName("downloadable")]
		public bool Downloadable { get; set; }
		[JsonPropertyName("marcFile")]
		public string MarcFile { get; set; }
		[JsonPropertyName("isMultiVolume")]
		public bool IsMultiVolume { get; set; }
		[JsonPropertyName("documentVolumeRefs")]
		public int DocumentVolumeRefs { get; set; }
		[JsonPropertyName("idOriginal")]
		public int IdOriginal { get; set; }
		[JsonPropertyName("isTranslated")]
		public bool IsTranslated { get; set; }
	}
}
