using AutoMapper;
using LibraryWebAPI.Dtos.BookDto;
using LibraryWebAPI.Dtos.RefreshToken;
using System;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Profiles
{
    public class Token_Profile : Profile
    {
        public Token_Profile()
        {
            CreateMap<RefreshToken_PostDto, Token>();
                
        }
    }
}
