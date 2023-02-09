using AutoMapper;
using LibraryWebAPI.Abstract.BookPhoto;
using LibraryWebAPI.Dtos.BookDto;
using LibraryWebAPI.Dtos.BookPhotoDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Interfaces;
using LibraryWebAPI.Parameters.Book;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infra.Data.Data;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Services
{
    public class BookPhotoService
    {
        private readonly LibraryDbContext _context;
        private readonly UploadFileService _uploadFileService;

        public BookPhotoService(LibraryDbContext context, UploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public bool Check(Guid id)
        {
            return _context.BookPhotos.Any(x => x.Id == id);
        }

        /// <summary>
        /// 刪除書籍圖片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(Guid id)
        {
            var entity = await _context.BookPhotos.SingleOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                await _uploadFileService.Delete(entity.UploadFileId);

                _context.BookPhotos.Remove(entity);
                _context.SaveChanges();
            }
        }


        public async Task<bool> Add(BookPhoto_PostDto entity)
        {
            var bookPhotoList = new List<BookPhoto>();
            //如果有附檔
            if (entity.Files != null)
            {
                List<Guid> guidList;
                var result = _uploadFileService.AddMultiple(entity.Files, out guidList);

                foreach (var id in guidList)
                {

                    BookPhoto bookPhoto = new BookPhoto()
                    {
                        Id = Guid.NewGuid(),
                        BookId = entity.BookId,
                        UploadFileId = id,
                        SystemDate = DateTime.Now
                    };

                    bookPhotoList.Add(bookPhoto);
                }
            }


            try
            {
                await _context.BookPhotos.AddRangeAsync(bookPhotoList);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //新增失敗
                return false;
            }

            return true;
        }


        public async Task<List<BookPhoto_GetDto>> GetBookPhotoDto(Guid bookId)
        {
            //轉換成Dto
            var bookPhotoDtoList = new List<BookPhoto_GetDto>();

            //書籍封面圖片
            bookPhotoDtoList = await (from b in _context.Books
                                          join bp in _context.BookPhotos on b.Id equals bp.BookId
                                          join u in _context.UploadFiles on bp.UploadFileId equals u.Id into bpu
                                          from j in bpu.DefaultIfEmpty()
                                          where b.Id == bookId
                                          let fileCompleteName = $"{j.Name ?? string.Empty}{j.Extension ?? string.Empty}"
                                          select new BookPhoto_GetDto()
                                          {
                                              Id = bp.Id,
                                              UploadFileId = bp.UploadFileId,
                                              Name = j.Name ?? string.Empty,
                                              Extension = j.Extension ?? string.Empty,
                                              FileCompleteName = fileCompleteName
                                          }).ToListAsync();

            return bookPhotoDtoList;
        }
    }
}
