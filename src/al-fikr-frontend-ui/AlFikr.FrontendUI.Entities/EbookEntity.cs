using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlFikr.FrontendUI.Entities
{
    public class EbookEntity : DocumentEntity
    {
        [JsonPropertyName("editionNum")]
        public string EditionNum { get; set; }
        [JsonPropertyName("editionPlace")]
        public string EditionPlace { get; set; }
        [Required(ErrorMessage = "Champ Obligatoire")]
        [JsonPropertyName("isbn")]
        public string ISBN { get; set; }
        [JsonPropertyName("genre")]
        public string Genre { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Champ Obligatoire")]
        [JsonPropertyName("nbPages")]
        public int NbPages { get; set; }
    }
}
