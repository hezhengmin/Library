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
    }
}
