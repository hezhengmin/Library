using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using Zheng.Infra.Data.Models;

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
