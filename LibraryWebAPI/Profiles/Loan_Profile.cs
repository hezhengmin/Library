using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Profiles
{
    public class Loan_Profile : Profile
    {
        public Loan_Profile()
        {
            CreateMap<Loan_PostDto, Loan>();
        }
    }
}
