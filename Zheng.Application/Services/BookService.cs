using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<List<Book>> Get(string title, DateTime? createAt)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.Contains(title));
            }

            if (createAt != null)
            {
                query = query.Where(x => x.CreatedAt.Date == createAt);
            }

            return await query.ToListAsync();
        }
    }
}
