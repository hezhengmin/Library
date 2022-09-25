using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Profiles
{
    public class Loan_PostProfile : Profile
    {
        public Loan_PostProfile()
        {
            CreateMap<Loan_PostDto, Loan>();
        }
    }
}
