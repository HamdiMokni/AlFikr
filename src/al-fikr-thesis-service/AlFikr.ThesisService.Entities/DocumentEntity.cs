using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AlFikr.ThesisService.Entities
{
	public class DocumentEntity
	{
		[JsonPropertyName("id")]
		public int? Id { get; set; }
		[JsonPropertyName("idEditor")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public int? IdEditor { get; set; }
		//[JsonPropertyName("editor")]
		//public EditorEntity? Editor { get; set; }
		[JsonPropertyName("idCollection")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public int? IdCollection { get; set; }
		//[JsonPropertyName("collection")]
		//public CollectionEntity? Collection { get; set; }
		[JsonPropertyName("mainAuthorsIds")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string[]? MainAuthorsIds { get; set; }
		[JsonPropertyName("secondaryAuthorsIds")]
		public string[]? SecondaryAuthorsIds { get; set; }
		//[JsonPropertyName("authors")]
		//public List<AuthorEntity>? Authors { get; set; }
		[JsonPropertyName("themesIds")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string[]? ThemesIds { get; set; }
		//[JsonPropertyName("themes")]
		//public List<ThemeEntity>? Themes { get; set; }
		[JsonPropertyName("cataloguesIds")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string[]? CataloguesIds { get; set; }
		//[JsonPropertyName("catalogues")]
		//public List<CatalogueEntity>? Catalogues { get; set; }
		[JsonPropertyName("doi")]
		public string? Doi { get; set; }
		[JsonPropertyName("marcRecordNumber")]
		public string? MarcRecordNumber { get; set; }
		[JsonPropertyName("originalTitle")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string? OriginalTitle { get; set; }
		[JsonPropertyName("titlesVariants")]
		public string? TitlesVariants { get; set; }
		[JsonPropertyName("subtitle")]
		public string? Subtitle { get; set; }
		[JsonPropertyName("foreword")]
		//[Required(ErrorMessage = "Champ Obligatoire")]
		public string? Foreword { get; set; }
		[JsonPropertyName("keywords")]
		//[Required(ErrorMessage = "Champ Obligatoire")]
		public string? Keywords { get; set; }
		[JsonPropertyName("fileDocument")]
		public string? FileDocument { get; set; }
		[JsonPropertyName("fileFormat")]
		public string? FileFormat { get; set; }
		[JsonPropertyName("coverPage")]
		public string? CoverPage { get; set; }
		[JsonPropertyName("url")]
		public string? Url { get; set; }
		[JsonPropertyName("documentType")]
		public string? DocumentType { get; set; }
		[JsonPropertyName("originalLanguage")]
		[Required(ErrorMessage = "Champ Obligatoire")]
		public string? OriginalLanguage { get; set; }
		[JsonPropertyName("languagesVariants")]
		public string? LanguagesVariants { get; set; }
		[JsonPropertyName("translator")]
		public string? Translator { get; set; }
		[JsonPropertyName("accessType")]
		public string? AccessType { get; set; }
		[JsonPropertyName("state")]
		public string? State { get; set; }
		[JsonPropertyName("publicationDate")]
		//[Required(ErrorMessage = "Champ Obligatoire")]
		public string? PublicationDate { get; set; }
		[JsonPropertyName("country")]
		public string? Country { get; set; }
		[JsonPropertyName("physicalDescription")]
		public string? PhysicalDescription { get; set; }
		[JsonPropertyName("accompanyingMaterials")]
		public string? AccompanyingMaterials { get; set; }
		[JsonPropertyName("accompanyingMaterialsNb")]
		public string? AccompanyingMaterialsNb { get; set; }
		[JsonPropertyName("volumeNb")]
		public int? VolumeNb { get; set; }
		[JsonPropertyName("abstract")]
		//[Required(ErrorMessage = "Champ Obligatoire")]
		public string? Abstract { get; set; }
		[JsonPropertyName("notes")]
		public string? Notes { get; set; }
		[JsonPropertyName("blankPages")]
		public int? BlankPages { get; set; }
		[JsonPropertyName("downloadable")]
		public bool Downloadable { get; set; }
		[JsonPropertyName("marcFile")]
		public string? MarcFile { get; set; }
		[JsonPropertyName("isMultiVolume")]
		public bool IsMultiVolume { get; set; }
		[JsonPropertyName("documentVolumeRefs")]
		public int? DocumentVolumeRefs { get; set; }
		[JsonPropertyName("idOriginal")]
		public int? IdOriginal { get; set; }
		[JsonPropertyName("isTranslated")]
		public bool IsTranslated { get; set; }

	}
}
