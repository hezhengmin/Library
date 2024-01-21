using AutoMapper;
using LibraryWebAPI.Dtos.BookDto;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Profiles
{
    public class Book_Profile : Profile
    {
        public Book_Profile()
        {
            CreateMap<Book_PostDto, Book>();
        }
    }
}
