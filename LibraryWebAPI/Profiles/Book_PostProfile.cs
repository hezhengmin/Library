using AutoMapper;
using LibraryWebAPI.Dtos.BookDto;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Profiles
{
    public class Book_PostProfile : Profile
    {
        public Book_PostProfile()
        {
            CreateMap<Book_PostDto, Book>();
        }
    }
}
