using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryWebAPI.Dtos.Book;
using LibraryWebAPI.Parameters.Book;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;
using LibraryWebAPI.Interfaces;

namespace LibraryWebAPI.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;

        public BookService(LibraryDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<Book> Get(Guid id)
        {
            return await _context.Books.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> Get(BookSelectParameter filter)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.title))
            {
                query = query.Where(x => x.Title.Contains(filter.title));
            }

            if (filter.createAt != null)
            {
                query = query.Where(x => x.CreatedAt.Date == filter.createAt);
            }

            return await query.ToListAsync();
        }

        public async Task<Book> Add(Book_AddDto entity)
        {
            var Book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = entity.Title,
                Author = entity.Author,
                Isbn = entity.Isbn,
                Status = entity.Status,
                CreatedAt = DateTime.Now,
                CreatedBy = _userService.CurrentAccountId,
                UpdatedAt = DateTime.Now,
                UpdatedBy = _userService.CurrentAccountId,
            };

            try
            {
                await _context.Books.AddAsync(Book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return null;
            }

            return Book;
        }

        public async Task<bool> Exits(string accountId)
        {
            return await _context.Accounts.AnyAsync(x => x.AccountId == accountId);
        }
    }
}
