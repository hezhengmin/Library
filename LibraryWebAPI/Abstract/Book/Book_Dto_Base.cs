using LibraryWebAPI.Abstract.BookPhoto;
using System.Collections.Generic;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_Dto_Base
    {
        /// <summary>
        /// 書名標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        public string Isbn { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }
        public abstract List<BookPhoto_Dto_Base> BookPhotos { get; set; } 
    }
}
