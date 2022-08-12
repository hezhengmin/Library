using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zheng.Application.Dtos.Book;
using Zheng.Application.Parameters.Book;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;

namespace Zheng.Application.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
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
                CreatedBy = Guid.NewGuid(),
                UpdatedAt = DateTime.Now,
                UpdatedBy = Guid.NewGuid()
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
