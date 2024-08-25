using AlFikr.BookService.Data.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlFikr.BookService.Entities.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<EbookEntity, Ebook>().ReverseMap();
        CreateMap<EbookEntity, Document>().ReverseMap();
        CreateMap<Author, AuthorEntity>().ReverseMap();
    }
}
