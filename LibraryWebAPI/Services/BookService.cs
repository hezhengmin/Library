using AutoMapper;
using Dapper;
using LibraryWebAPI.Dtos.BookDto;
using LibraryWebAPI.Dtos.BookPhotoDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Interfaces;
using LibraryWebAPI.Parameters.Book;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infra.Data.Data;
using Zheng.Infra.Data.Models;
using Zheng.Utilities.Helpers;

namespace LibraryWebAPI.Services
{
    public class BookService
    {
        private readonly LibraryDbContext _context;
        private readonly IUserService _userService;
        private readonly UploadFileService _uploadFileService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public BookService(LibraryDbContext context, IUserService userService, UploadFileService uploadFileService, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _userService = userService;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
            _env = env;
        }
        public async Task<Book_GetDto> GetDto(Guid id)
        {
            var book = await _context.Books
                .Include(x => x.BookPhotos)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (book == null) return null;



            //轉換成Dto
            var bookPhoto_GetDto = new List<BookPhoto_GetDto>();

            //書籍封面圖片
            var bookPhotoDtoList = (from b in _context.Books
                                    join bp in _context.BookPhotos on b.Id equals bp.BookId
                                    join u in _context.UploadFiles on bp.UploadFileId equals u.Id into bpu
                                    from j in bpu.DefaultIfEmpty()
                                    where b.Id == id
                                    let fileCompleteName = $"{j.Name ?? string.Empty}{j.Extension ?? string.Empty}"
                                    select new BookPhoto_GetDto()
                                    {
                                        Id = bp.Id,
                                        UploadFileId = bp.UploadFileId,
                                        Name = j.Name ?? string.Empty,
                                        Extension = j.Extension ?? string.Empty,
                                        FileCompleteName = fileCompleteName
                                    }).ToList();

            return new Book_GetDto
            {
                Id = book.Id,
                BookPhotos = bookPhotoDtoList,
                Title = book.Title,
                Status = book.Status,
                NumberOfCopies = book.NumberOfCopies,
                Isbn = book.Isbn,
                Issn = book.Issn,
                Gpn = book.Gpn,
                Publisher = book.Publisher,
                RightCondition = book.RightCondition,
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
            var x = _userService.CurrentUserId;

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

            //排序在分頁前
            query = query.OrderByDescending(x => x.CreatedAt);

            if (filter.PaginationResult != null)
            {
                query = query.Skip((filter.PaginationResult.PageNumber - 1) * filter.PaginationResult.PageSize)
                               .Take(filter.PaginationResult.PageSize);
            }

            var List = await query.Select(x => new Book_GetDto
            {
                Id = x.Id,
                BookPhotos = null,
                Title = x.Title,
                Status = x.Status,
                NumberOfCopies = x.NumberOfCopies,
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
                CeasedDate = x.CeasedDate,
                Authority = x.Authority
            }).ToListAsync();

            return new PagedResponse<List<Book_GetDto>>
            {
                Data = List,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)filter.PaginationResult.PageSize)
            };
        }
        public async Task<Book> Add(Book_PostDto entity)
        {
            var book = _mapper.Map<Book>(entity);

            book.Id = Guid.NewGuid();
            book.CreatedAt = DateTime.Now;
            book.CreatedBy = _userService.CurrentUserId;
            book.UpdatedAt = DateTime.Now;
            book.UpdatedBy = _userService.CurrentUserId;

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

        /// <summary>
        /// 更新書籍(包含新增書籍圖片)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> Update(Book_PutDto entity)
        {
            var book = await Get(entity.Id);

            if (book == null) return false;

            //更新欄位
            book.Title = entity.Title;
            book.Status = entity.Status;
            book.NumberOfCopies = entity.NumberOfCopies;
            book.Isbn = entity.Isbn;
            book.Issn = entity.Issn;
            book.Gpn = entity.Gpn;
            book.Publisher = entity.Publisher;
            book.RightCondition = entity.RightCondition;
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
            book.UpdatedBy = _userService.CurrentUserId;

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
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 刪除書籍(包含BookPhoto)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            var entity = await _context.Books
                .Include(x=>x.Loans)
                .Include(x => x.BookPhotos).SingleOrDefaultAsync(x => x.Id == id);

            foreach (var photo in entity.BookPhotos)
            {
                var fileId = photo.UploadFileId;
                await _uploadFileService.Delete(fileId);
            }

            _context.Loans.RemoveRange(entity.Loans);
            _context.BookPhotos.RemoveRange(entity.BookPhotos);
            _context.Books.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// 書籍_下拉式選單
        /// </summary>
        /// <returns></returns>
        public async Task<List<Book_SelectListDto>> GetSelectList()
        {
            // 設計SQL語法
            string strSqlQuery = @"
            select 
            --B.NumberOfCopies as '總共幾本',
            --ISNULL(L.cnt,0) as '借閱幾本', 
            convert(nvarchar(36), lower(B.Id)) as Id, /*guid 轉換成字串*/
            B.Title +' (剩餘' + Cast((B.NumberOfCopies- ISNULL(L.cnt,0)) as varchar(10)) + '本)' as 'Text'
            from Book B
            left join (
              select BookId,count(*) as CNT from Loan
              where ReturnDate is null or ReturnDate > GETDATE()
              group by BookId
            ) L on B.Id = L.BookId 
            order by B.NumberOfCopies - ISNULL(L.cnt,0) desc
            ";

            await using var connection = _context.Database.GetDbConnection();
            await _context.Database.OpenConnectionAsync();

            var list = new List<Book_SelectListDto>();
            try
            {
                list = (List<Book_SelectListDto>)await connection
                    .QueryAsync<Book_SelectListDto>(strSqlQuery);
            }
            catch (Exception ex)
            {

            }

            list.Insert(0, new Book_SelectListDto() { Id = Guid.Empty.ToString(), Text = "請選擇" });

            return list;
        }

        /// <summary>
        /// 剩下書籍數量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetRemainBookCount(Guid id)
        {
            int bookStockCount = 0, countLoan = 0;

            var book = await Get(id);
            bookStockCount = book.NumberOfCopies;

            //現在借閱的數量
            countLoan = await _context.Loans
                .Where(x=>x.ReturnDate == null || x.ReturnDate < DateTime.Now)
                .CountAsync(x => x.BookId == id);

            return bookStockCount - countLoan;
        }

        /// <summary>
        /// 最少庫存數量
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<int> GetLeastNumberOfCopiesCount(Guid bookId)
        {
            int countLoan = 0;

            countLoan = await _context.Loans.CountAsync(x => x.BookId == bookId);

            return countLoan;
        }

        /// <summary>
        /// 書籍列表_匯出Excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportExcel(BookSelectParameter filter)
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

            //排序在分頁前
            query = query.OrderByDescending(x => x.CreatedAt);

            var list = await query.Select(x => new Book_GetDto
            {
                Id = x.Id,
                BookPhotos = null,
                Title = x.Title,
                Status = x.Status,
                NumberOfCopies = x.NumberOfCopies,
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
                CeasedDate = x.CeasedDate,
                Authority = x.Authority
            }).ToListAsync();

            var filePath = $"{_env.ContentRootPath}\\Template\\Excel\\書籍列表匯出範本.xlsx";

            using (var source = System.IO.File.OpenRead(filePath))
            {
                using (var package = new ExcelPackage(source))
                {
                    //頁籤
                    var sheet = package.Workbook.Worksheets["書籍"];

                    #region 儲存格讀寫
                    int row = 2, col = 1;
                    foreach (var item in list)
                    {
                        col = 1;

                        /*
                        EPPlus儲存格是從[1,1]開始
                        寫入資料，[行，列]
                        */
                        sheet.Cells[row, col++].Value = item.Title;

                        var _status = (Book.StatusType)item.Status;

                        sheet.Cells[row, col++].Value = EnumHelper<Book.StatusType>.GetDisplayValue(_status);
                        sheet.Cells[row, col++].Value = item.NumberOfCopies;
                        sheet.Cells[row, col++].Value = item.Isbn;
                        sheet.Cells[row, col++].Value = item.Issn;
                        sheet.Cells[row, col++].Value = item.Gpn;
                        sheet.Cells[row, col++].Value = item.Publisher;
                        sheet.Cells[row, col++].Value = item.RightCondition;
                        sheet.Cells[row, col++].Value = item.Creator;
                        sheet.Cells[row, col++].Value = item.PublishDate.ToShortDateString();
                        sheet.Cells[row, col++].Value = item.Edition;
                        sheet.Cells[row, col++].Value = item.Cover;
                        sheet.Cells[row, col++].Value = item.Classify;
                        sheet.Cells[row, col++].Value = item.Gpntype;
                        sheet.Cells[row, col++].Value = item.Subject;
                        sheet.Cells[row, col++].Value = item.Governance;
                        sheet.Cells[row, col++].Value = item.Grade;
                        sheet.Cells[row, col++].Value = item.Pages;
                        sheet.Cells[row, col++].Value = item.Size;
                        sheet.Cells[row, col++].Value = item.Binding;
                        sheet.Cells[row, col++].Value = item.Language;
                        sheet.Cells[row, col++].Value = item.Introduction;
                        sheet.Cells[row, col++].Value = item.Catalog;
                        sheet.Cells[row, col++].Value = item.Price;
                        sheet.Cells[row, col++].Value = item.TargetPeople;
                        sheet.Cells[row, col++].Value = item.Types;
                        sheet.Cells[row, col++].Value = item.Attachment;
                        sheet.Cells[row, col++].Value = item.Url;
                        sheet.Cells[row, col++].Value = item.Duration;
                        sheet.Cells[row, col++].Value = item.Numbers;
                        sheet.Cells[row, col++].Value = item.Restriction;
                        sheet.Cells[row, col++].Value = item.CeasedDate;
                        sheet.Cells[row, col++].Value = item.Authority;
                        row++;
                    }
                    #endregion


                    #region 樣式
                    using (var range = sheet.Cells[2, 1, row - 1, 33])
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

        /// <summary>
        /// 匯入Excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<CommonResponse> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new CommonResponse()
                {
                    Errors = new List<string>() { "檔案上傳有誤" },
                    Success = false,
                    Data = null
                };
            }

            List<Book_PostDto> list = new List<Book_PostDto>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var entity = new Book_PostDto();
                        int col = 1;
                        entity.Title = worksheet.Cells[row, col++].Value.ToString().Trim();

                        int _status = -1;
                        switch (worksheet.Cells[row, col++].Value?.ToString().Trim())
                        {
                            case "無庫存":
                                _status = 0;
                                break;
                            case "有庫存":
                                _status = 1;
                                break;
                            default:
                                _status = -1;
                                break;
                        }
                        entity.Status = _status;

                        int _numberOfCopies = 0;
                        int.TryParse(worksheet.Cells[row, col++].Value.ToString().Trim(), out _numberOfCopies);
                        entity.NumberOfCopies = _numberOfCopies;

                        entity.Isbn = worksheet.Cells[row, col++].Value.ToString().Trim();
                        entity.Issn = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Gpn = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Publisher = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.RightCondition = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Creator = worksheet.Cells[row, col++].Value.ToString().Trim();

                        //出版日期
                        DateTime _publishDate;
                        string dateString = worksheet.Cells[row, col++].Value.ToString().Trim();

                        if (DateTime.TryParse(dateString, out _publishDate))
                        {
                            //TryParse轉換成功
                        }
                        //DateTime.TryParseExact(dateString, "yyyy/MM/dd", null, 
                        //    DateTimeStyles.None, out _publishDate);
                        entity.PublishDate = _publishDate;

                        entity.Edition = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Cover = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Classify = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Gpntype = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Subject = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Governance = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Grade = worksheet.Cells[row, col++].Value?.ToString().Trim();

                        int _pages = 0;
                        int.TryParse(worksheet.Cells[row, col++].Value.ToString().Trim(), out _pages);
                        entity.Pages = _pages;

                        entity.Size = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Binding = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Language = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Introduction = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Catalog = worksheet.Cells[row, col++].Value?.ToString().Trim();


                        int _price = 0;
                        int.TryParse(worksheet.Cells[row, col++].Value.ToString().Trim(), out _price);
                        entity.Price = _price;

                        entity.TargetPeople = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Types = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Attachment = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Url = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Duration = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Numbers = worksheet.Cells[row, col++].Value?.ToString().Trim();
                        entity.Restriction = worksheet.Cells[row, col++].Value?.ToString().Trim();

                        var ceasedDate = worksheet.Cells[row, col++].Value?.ToString().Trim();

                        if (string.IsNullOrEmpty(ceasedDate))
                        {
                            entity.CeasedDate = null;
                        }
                        else
                        {
                            DateTime _ceasedDate;
                            DateTime.TryParse(ceasedDate, out _ceasedDate);
                            entity.CeasedDate = _ceasedDate;
                        }

                        entity.Authority = worksheet.Cells[row, col++].Value.ToString().Trim();

                        list.Add(entity);
                    }
                }
            }

            return new CommonResponse()
            {
                Success = true,
                Data = list
            };
        }

        /// <summary>
        /// 批次新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<CommonResponse> AddMultiple(Book_MultiplePostDto entity)
        {
            var list = new List<Book_PostDto>();

            if (entity.Books != null)
            {
                foreach (var book in entity.Books)
                {
                    var result = await Add(book);

                    //新增失敗，加入清單
                    if (result == null)
                    {
                        list.Add(book);
                    }
                }
            }


            if (list.Count > 0)
            {
                return new CommonResponse()
                {
                    Success = false,
                    Data = list
                };
            }

            return new CommonResponse()
            {
                Success = true,
                Data = null
            };
        }


        /// <summary>
        /// 匯入範本(供下載用)
        /// </summary>
        /// <returns></returns>
        public async Task<MemoryStream> ImportExcelExample()
        {
            var filePath = $"{_env.ContentRootPath}\\Template\\Excel\\書籍列表匯入範本.xlsx";

            var memory = new MemoryStream();

            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return memory;
        }
    }
}
