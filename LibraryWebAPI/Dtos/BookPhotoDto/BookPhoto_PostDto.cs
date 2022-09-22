using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.BookPhotoDto
{
    public class BookPhoto_PostDto
    {
        public Guid BookId { get; set; }

        /// <summary>
        /// 多檔或單一檔案
        /// </summary>
        public ICollection<IFormFile> Files { get; set; }
    }
}
