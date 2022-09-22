using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Dtos.BookPhotoDto;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.BookDto
{
    public class Book_GetDto : Book_Dto_Base
    {
        public List<BookPhoto_GetDto> BookPhotos { get; set; } = new List<BookPhoto_GetDto>();
    }
}
