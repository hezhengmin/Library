using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Interfaces;
using System.Threading.Tasks;
using System;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Services
{
    public class LoanService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoanService(LibraryDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Loan_GetDto> GetDto(Guid id)
        {
            var loan = await _context.Loans
                .SingleOrDefaultAsync(x => x.Id == id);

            if (loan == null) return null;


            //var loan_GetDto = from b in _context.Loans
            //                        select new Loan_GetDto() {
            //                        Id = }

                                

            return null;
        }


        public async Task<Loan> Add(Loan_PostDto entity)
        {
            var Loan = _mapper.Map<Loan>(entity);

            Loan.Id = Guid.NewGuid();
            Loan.CreatedAt = DateTime.Now;
            Loan.CreatedBy = _userService.CurrentUserId;
            Loan.UpdatedAt = DateTime.Now;
            Loan.UpdatedBy = _userService.CurrentUserId;

            try
            {
                await _context.Loans.AddAsync(Loan);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return null;
            }

            return Loan;
        }
    }
}
