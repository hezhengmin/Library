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
using LibraryWebAPI.Dtos.BookPhoto;
using LibraryWebAPI.Abstract.BookPhoto;

namespace LibraryWebAPI.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly UploadFileService _uploadFileService;

        public BookService(LibraryDbContext context, IUserService userService, UploadFileService uploadFileService)
        {
            _context = context;
            _userService = userService;
            _uploadFileService = uploadFileService;
        }

        public async Task<Book_GetDto> GetDto(Guid id)
        {
            var book = await _context.Books
                .Include(x => x.BookPhotos)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (book == null) return null;

            //轉換成Dto
            var bookPhoto_GetDto = new List<BookPhoto_Dto_Base>();
            foreach (var item in book.BookPhotos)
            {
                var bookPhoto = new BookPhoto_Dto_Base()
                {
                    UploadFileId = item.UploadFileId,
                };
                bookPhoto_GetDto.Add(bookPhoto);
            }

            return new Book_GetDto
            {
                Id = book.Id,
                Author = book.Author,
                BookPhotos = bookPhoto_GetDto,
                Isbn = book.Isbn,
                Status = book.Status,
                Title = book.Title,
            };
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
            var query = _context.Books
                .Include(x => x.BookPhotos)
                .AsQueryable();

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

        /// <summary>
        /// 書籍列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<Book_GetDto>> GetList(BookSelectParameter filter)
        {
            var query = _context.Books
                .Include(x => x.BookPhotos)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.title))
            {
                query = query.Where(x => x.Title.Contains(filter.title));
            }

            if (filter.createAt != null)
            {
                query = query.Where(x => x.CreatedAt.Date == filter.createAt);
            }

            return await query.Select(x => new Book_GetDto
            {
                Id = x.Id,
                Author = x.Author,
                BookPhotos = x.BookPhotos.Select(y => new BookPhoto_Dto_Base
                {
                    UploadFileId = y.UploadFileId
                }).ToList(),
                Isbn = x.Isbn,
                Status = x.Status,
                Title = x.Title,
            }).ToListAsync();
        }

        public async Task<Book> Add(Book_PostDto entity)
        {
            var book = new Book()
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

            //如果有附檔
            if (entity.Files != null)
            {
                List<Guid> guidList;
                var result = _uploadFileService.AddMultiple(entity.Files, out guidList);

                foreach (var id in guidList)
                {
                    book.BookPhotos.Add(new BookPhoto()
                    {
                        Id = Guid.NewGuid(),
                        UploadFileId = id,
                        SystemDate = DateTime.Now,
                    });

                }
            }

            try
            {
                await _context.Books.AddAsync(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return null;
            }

            return book;
        }

        public async Task<bool> Exits(string accountId)
        {
            return await _context.Accounts.AnyAsync(x => x.AccountId == accountId);
        }
    }
}
