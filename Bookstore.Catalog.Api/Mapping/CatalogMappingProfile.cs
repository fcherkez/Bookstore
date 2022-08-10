using AutoMapper;
using Bookstore.Catalog.Api.Dto.Books;
using Bookstore.Catalog.Api.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Catalog.Api.Mapping
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<Book, BookResponse>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorResponse>().ReverseMap();
            CreateMap<BookAuthor, BookResponse>().AfterMap(((src, dest, ctx) => ctx.Mapper.Map(src.Book, dest)));
            CreateMap<BookGenre, BookGenreResponse>().ReverseMap();
            CreateMap<BookGenre, BookResponse>().AfterMap(((src, dest, ctx) => ctx.Mapper.Map(src.Book, dest)));
            CreateMap<Genre, GenreResponse>().ReverseMap();
            CreateMap<Author, AuthorResponse>().ReverseMap();
            CreateMap<BookRequest, Book>().
                ForMember(x => x.Authors,
                x => x.MapFrom(
                y => y.Authors.Select(z => new BookAuthor()
                {
                    AuthorID = z
                })
                )
                )
                .ForMember(x => x.Genres,
                x => x.MapFrom(
                y => y.Genres.Select(
                z => new BookGenre()
                {
                    GenreID = z
                })
                )
                )
                .ReverseMap();
        }
    }
}
    

