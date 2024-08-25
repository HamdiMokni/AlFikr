using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities
{
    public class EbookEntity : DocumentEntity
    {
        public string EditionNum { get; set; }
        public string EditionPlace { get; set; }
        [Required(ErrorMessage = "Champ Obligatoire")]
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Champ Obligatoire")]
        public int NbPages { get; set; }
    }
}
