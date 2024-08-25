using System.ComponentModel.DataAnnotations;

namespace AlFikr.BookService.Entities
{
    public class CatalogueEntity
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public string? OwnerType { get; set; }
        public string? OwnerName { get; set; }

        [Required(ErrorMessage = "Champ Obligatoire")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Champ Obligatoire")]
        public string? ArTitle { get; set; }

        [Required(ErrorMessage = "Champ Obligatoire")]
        public string? ShortTitle { get; set; }

        [Required(ErrorMessage = "Champ Obligatoire")]
        public string? Description { get; set; }

        public CatalogueEntity()
        { }

        public CatalogueEntity(int id, int idOwner, string ownerType, string ownerName, string title, string arTitle, string shortTitle, string description)
        {
            Id = id;
            IdOwner = idOwner;
            OwnerType = ownerType;
            OwnerName = ownerName;
            Title = title;
            ArTitle = arTitle;
            ShortTitle = shortTitle;
            Description = description;
        }
    }
}
