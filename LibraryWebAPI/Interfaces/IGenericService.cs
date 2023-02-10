using System.Threading.Tasks;
using System;
using Zheng.Infra.Data.Models;
using LibraryWebAPI.Dtos.LoanDto;

namespace LibraryWebAPI.Interfaces
{
    public interface IGenericService
    {
        Task<Loan> Add(Loan_PostDto entity);
    }
}
