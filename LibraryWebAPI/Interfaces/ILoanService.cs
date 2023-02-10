using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Parameters.Loan;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Interfaces
{
    public interface ILoanService : IGenericService
    {
        Task<Loan_GetDto> GetDto(Guid id);
        Task<PagedResponse<List<Loan_GetDto>>> Get(LoanSelectParameter filter);
        Task<Loan> Add(Loan_PostDto entity);
        Task<Loan> Get(Guid id);
        bool Check(Guid id);
        Task<bool> Update(Loan_PutDto entity);
        Task Delete(Guid id);
        Task<byte[]> ExportExcel(LoanSelectParameter filter);
        Task<Loan> GetLast();
    }
}
