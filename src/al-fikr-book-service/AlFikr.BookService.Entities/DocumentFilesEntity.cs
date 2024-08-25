using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities
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
