using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Abstract.BookPhoto;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Book
{
    public class Book_GetDto : Book_Dto_Base
    {
        public Guid Id { get; set; }
        public List<BookPhoto_Dto_Base> BookPhotos { get; set; } = new List<BookPhoto_Dto_Base>();
    }
}
