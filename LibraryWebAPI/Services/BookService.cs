using AutoMapper;
using LibraryWebAPI.Abstract.BookPhoto;
using LibraryWebAPI.Dtos.BookDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Interfaces;
using LibraryWebAPI.Parameters.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly UploadFileService _uploadFileService;
        private readonly IMapper _mapper;

        public BookService(LibraryDbContext context, IUserService userService, UploadFileService uploadFileService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
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
                BookPhotos = bookPhoto_GetDto,
                Title = book.Title,
                Status = book.Status,
                Isbn = book.Isbn,
                Issn = book.Issn,
                Gpn = book.Gpn,
                Publisher = book.Publisher,
                RightCondition = book.Restriction,
                Creator = book.Creator,
                PublishDate = book.PublishDate,
                Edition = book.Edition,
                Cover = book.Cover,
                Classify = book.Classify,
                Gpntype = book.Gpntype,
                Subject = book.Subject,
                Governance = book.Governance,
                Grade = book.Grade,
                Pages = book.Pages,
                Size = book.Size,
                Binding = book.Binding,
                Language = book.Language,
                Introduction = book.Introduction,
                Catalog = book.Catalog,
                Price = book.Price,
                TargetPeople = book.TargetPeople,
                Types = book.Types,
                Attachment = book.Attachment,
                Url = book.Url,
                Duration = book.Duration,
                Numbers = book.Numbers,
                Restriction = book.Restriction,
                CeasedDate = book.CeasedDate,
                Authority = book.Authority
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

        /// <summary>
        /// 書籍列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<PagedResponse<List<Book_GetDto>>> Get(BookSelectParameter filter)
        {
            var query = _context.Books
                .Include(x => x.BookPhotos)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x => x.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrWhiteSpace(filter.Isbn))
            {
                query = query.Where(x => x.Isbn.Contains(filter.Isbn));
            }

            if (!string.IsNullOrWhiteSpace(filter.Edition))
            {
                query = query.Where(x => x.Edition.Contains(filter.Edition));
            }

            if (filter.CreateAt != null)
            {
                query = query.Where(x => x.CreatedAt.Date == filter.CreateAt);
            }

            var totalRecords = query.Count();


            if (filter.PaginationFilter != null)
            {
                query = query.Skip((filter.PaginationFilter.PageNumber - 1) * filter.PaginationFilter.PageSize)
                               .Take(filter.PaginationFilter.PageSize);
            }

            var List = await query.Select(x => new Book_GetDto
            {
                Id = x.Id,
                BookPhotos = x.BookPhotos.Select(y => new BookPhoto_Dto_Base
                {
                    UploadFileId = y.UploadFileId
                }).ToList(),
                Title = x.Title,
                Status = x.Status,
                Isbn = x.Isbn,
                Issn = x.Issn,
                Gpn = x.Gpn,
                Publisher = x.Publisher,
                RightCondition = x.Restriction,
                Creator = x.Creator,
                PublishDate = x.PublishDate,
                Edition = x.Edition,
                Cover = x.Cover,
                Classify = x.Classify,
                Gpntype = x.Gpntype,
                Subject = x.Subject,
                Governance = x.Governance,
                Grade = x.Grade,
                Pages = x.Pages,
                Size = x.Size,
                Binding = x.Binding,
                Language = x.Language,
                Introduction = x.Introduction,
                Catalog = x.Catalog,
                Price = x.Price,
                TargetPeople = x.TargetPeople,
                Types = x.Types,
                Attachment = x.Attachment,
                Url = x.Url,
                Duration = x.Duration,
                Numbers = x.Numbers,
                Restriction = x.Restriction,
                CeasedDate = x.CeasedDate
            }).ToListAsync();

            return new PagedResponse<List<Book_GetDto>>
            {
                Data = List,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PaginationFilter.PageSize)
            };
        }
        public async Task<Book> Add(Book_PostDto entity)
        {
            var book = _mapper.Map<Book>(entity);

            book.Id = Guid.NewGuid();
            book.CreatedAt = DateTime.Now;
            book.CreatedBy = _userService.CurrentAccountId;
            book.UpdatedAt = DateTime.Now;
            book.UpdatedBy = _userService.CurrentAccountId;

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
       

        public bool Check(Guid id)
        {
            return _context.Books.Any(x => x.Id == id);
        }

        public async Task<bool> Update(Book_PutDto entity)
        {
            var book = await Get(entity.Id);

            if (book == null) return false;

            //更新欄位
            book.Title = entity.Title;
            book.Status = entity.Status;
            book.Isbn = entity.Isbn;
            book.Issn = entity.Issn;
            book.Gpn = entity.Gpn;
            book.Publisher = entity.Publisher;
            book.RightCondition = entity.Restriction;
            book.Creator = entity.Creator;
            book.PublishDate = entity.PublishDate;
            book.Edition = entity.Edition;
            book.Cover = entity.Cover;
            book.Classify = entity.Classify;
            book.Gpntype = entity.Gpntype;
            book.Subject = entity.Subject;
            book.Governance = entity.Governance;
            book.Grade = entity.Grade;
            book.Pages = entity.Pages;
            book.Size = entity.Size;
            book.Binding = entity.Binding;
            book.Language = entity.Language;
            book.Introduction = entity.Introduction;
            book.Catalog = entity.Catalog;
            book.Price = entity.Price;
            book.TargetPeople = entity.TargetPeople;
            book.Types = entity.Types;
            book.Attachment = entity.Attachment;
            book.Url = entity.Url;
            book.Duration = entity.Duration;
            book.Numbers = entity.Numbers;
            book.Restriction = entity.Restriction;
            book.CeasedDate = entity.CeasedDate;
            book.Authority = entity.Authority;
            book.UpdatedAt = DateTime.Now;
            book.UpdatedBy = _userService.CurrentAccountId;

            try
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }
    }
}
