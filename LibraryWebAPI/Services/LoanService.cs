using AutoMapper;
using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using LibraryWebAPI.Dtos.Responses;
using System.Collections.Generic;
using LibraryWebAPI.Parameters.Loan;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LibraryWebAPI.Services
{
    public class LoanService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public LoanService(LibraryDbContext context, IUserService userService, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _env = env;
        }

        public async Task<Loan_GetDto> GetDto(Guid id)
        {
            var loan = await _context.Loans
                .SingleOrDefaultAsync(x => x.Id == id);

            if (loan == null) return null;

            var result = (from l in _context.Loans
                          join a in _context.Accounts on l.AccountId equals a.Id
                          join b in _context.Books on l.BookId equals b.Id
                          where l.Id == id
                          select new Loan_GetDto
                          {
                              Id = l.Id,
                              AccountId = l.AccountId,
                              BookId = l.BookId,
                              IssueDate = l.IssueDate,
                              DueDate = l.DueDate,
                              ReturnDate = l.ReturnDate,
                              BookTitle = b.Title,
                              UserId = a.UserId
                          }).SingleOrDefault();

            return result;
        }

        public async Task<PagedResponse<List<Loan_GetDto>>> Get(LoanSelectParameter filter)
        {
            var query = (from l in _context.Loans
                         join a in _context.Accounts on l.AccountId equals a.Id
                         join b in _context.Books on l.BookId equals b.Id
                         orderby l.CreatedAt descending
                         select new Loan_GetDto
                         {
                             Id = l.Id,
                             AccountId = l.AccountId,
                             BookId = l.BookId,
                             IssueDate = l.IssueDate,
                             DueDate = l.DueDate,
                             ReturnDate = l.ReturnDate,
                             BookTitle = b.Title,
                             UserId = a.UserId
                         })
                         .AsQueryable();


            if (!string.IsNullOrWhiteSpace(filter.BookTitle))
            {
                query = query.Where(x => x.BookTitle.Contains(filter.BookTitle));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserId))
            {
                query = query.Where(x => x.UserId.Contains(filter.UserId));
            }

            if (filter.IssueDate != null)
            {
                query = query.Where(x => x.IssueDate.Date >= filter.IssueDate);
            }

            if (filter.DueDate != null)
            {
                query = query.Where(x => x.IssueDate.Date <= filter.DueDate);
            }

            if (filter.ReturnDate != null)
            {
                query = query.Where(x => x.IssueDate.Date == filter.ReturnDate);
            }

            var totalRecords = query.Count();

            if (filter.PaginationResult != null)
            {
                query = query.Skip((filter.PaginationResult.PageNumber - 1) * filter.PaginationResult.PageSize)
                               .Take(filter.PaginationResult.PageSize);
            }

            return new PagedResponse<List<Loan_GetDto>>
            {
                Data = await query.ToListAsync(),
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PaginationResult.PageSize)
            };
        }

        /// <summary>
        /// 新增借閱
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        public async Task<Loan> Get(Guid id)
        {
            return await _context.Loans.SingleOrDefaultAsync(x => x.Id == id);
        }

        public bool Check(Guid id)
        {
            return _context.Loans.Any(x => x.Id == id);
        }

        /// <summary>
        /// 更新借閱
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(Loan_PutDto entity)
        {
            var loan = await Get(entity.Id);

            if (loan == null) return false;

            //更新欄位
            loan.AccountId = entity.AccountId;
            loan.BookId = entity.BookId;
            loan.IssueDate = entity.IssueDate;
            loan.DueDate = entity.DueDate;
            loan.ReturnDate = entity.ReturnDate;
            loan.UpdatedAt = DateTime.Now;
            loan.UpdatedBy = _userService.CurrentUserId;

            try
            {
                _context.Entry(loan).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }

        public async Task Delete(Guid id)
        {
            var entity = await Get(id);
  
            _context.Loans.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<byte[]> ExportExcel(LoanSelectParameter filter)
        {
            var query = (from l in _context.Loans
                         join a in _context.Accounts on l.AccountId equals a.Id
                         join b in _context.Books on l.BookId equals b.Id
                         orderby l.CreatedAt descending
                         select new Loan_GetDto
                         {
                             Id = l.Id,
                             AccountId = l.AccountId,
                             BookId = l.BookId,
                             IssueDate = l.IssueDate,
                             DueDate = l.DueDate,
                             ReturnDate = l.ReturnDate,
                             BookTitle = b.Title,
                             UserId = a.UserId
                         })
                         .AsQueryable();


            if (!string.IsNullOrWhiteSpace(filter.BookTitle))
            {
                query = query.Where(x => x.BookTitle.Contains(filter.BookTitle));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserId))
            {
                query = query.Where(x => x.UserId.Contains(filter.UserId));
            }

            if (filter.IssueDate != null)
            {
                query = query.Where(x => x.IssueDate.Date >= filter.IssueDate);
            }

            if (filter.DueDate != null)
            {
                query = query.Where(x => x.IssueDate.Date <= filter.DueDate);
            }

            if (filter.ReturnDate != null)
            {
                query = query.Where(x => x.IssueDate.Date == filter.ReturnDate);
            }

            var list = await query.ToListAsync();

            var filePath = $"{_env.ContentRootPath}\\Template\\Excel\\書籍借閱匯出範本.xlsx";

            using (var source = System.IO.File.OpenRead(filePath))
            {
                using (var package = new ExcelPackage(source))
                {
                    //頁籤
                    var sheet = package.Workbook.Worksheets["書籍借閱"];

                    #region 儲存格讀寫
                    int row = 2, col = 1;
                    foreach (var item in list)
                    {
                        col = 1;
                        /*
                        EPPlus儲存格是從[1,1]開始
                        寫入資料，[行，列]
                        */
                        sheet.Cells[row, col++].Value = item.BookTitle;
                        sheet.Cells[row, col++].Value = item.UserId;
                        sheet.Cells[row, col++].Value = item.IssueDate.ToShortDateString();
                        sheet.Cells[row, col++].Value = item.DueDate.ToShortDateString();
                        sheet.Cells[row, col++].Value = item.ReturnDate?.ToShortDateString();
                        row++;
                    }
                    #endregion


                    #region 樣式
                    using (var range = sheet.Cells[2, 1, row - 1, 5])
                    {
                        //表格加入框線
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        //字型
                        range.Style.Font.Name = "微軟正黑體";
                        //文字大小
                        range.Style.Font.Size = 12;
                    }
                    #endregion
                    var excelData = package.GetAsByteArray();
                    return excelData;
                }
            }
        }

    }
}
