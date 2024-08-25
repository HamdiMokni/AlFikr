using System.ComponentModel.DataAnnotations;

namespace AlFikr.FrontendUI.Entities
{
	public class DocumentFilesEntity
	{
		public int Id { get; set; }
		[Required]
		public int IdDocument { get; set; }
		[Required]
		public string Reference { get; set; }
		[Required]
		public string Title { get; set; }
		public string FileDocument { get; set; }
		public string FileFormat { get; set; }
		[Required]
		public int StartPage { get; set; }
		[Required]
		public int EndPage { get; set; }
		[Required]
		public string State { get; set; }
		public DateTime AddedOn { get; set; }
	}
}
