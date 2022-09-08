using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Abstract.BookPhoto;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.BookDto
{
    public class Book_GetDto : Book_Dto_Base
    {
        public List<BookPhoto_Dto_Base> BookPhotos { get; set; } = new List<BookPhoto_Dto_Base>();
    }
}
