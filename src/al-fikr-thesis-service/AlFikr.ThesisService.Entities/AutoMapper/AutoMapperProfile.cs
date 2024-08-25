using AlFikr.ThesisService.Data.Models;
using AutoMapper;

namespace AlFikr.ThesisService.Entities.AutoMapper;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<ThesisEntity, Thesis>().ReverseMap();
		CreateMap<ThesisEntity, Document>().ReverseMap();

		CreateMap<Supervisor, SupervisorEntity>().ReverseMap();


		//CreateMap<Author, AuthorEntity>().ForMember(dest => dest.Theses, opt => opt.MapFrom(src => src.Documentauthors.Select(x => x.IdDocumentNavigation))).ReverseMap();
		CreateMap<Editor, EditorEntity>().ReverseMap();
		CreateMap<Catalogue, CatalogueEntity>().ReverseMap();
		CreateMap<Theme, ThemeEntity>().ReverseMap();
		CreateMap<Collection, CollectionEntity>().ReverseMap();
	}
}
