using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Abstract.BookPhoto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Book
{
    public class Book_PostDto : Book_Dto_Base
    {
        /// <summary>
        /// 多檔或單一檔案
        /// </summary>
        public ICollection<IFormFile> Files { get; set; }
    }
}
