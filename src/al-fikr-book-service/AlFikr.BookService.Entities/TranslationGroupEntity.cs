namespace AlFikr.BookService.Entities;

public class TranslationGroupEntity
{
    public int Id { get; set; }

    public int IdDocument { get; set; }

    public int? IdGroupRefs { get; set; }
}
