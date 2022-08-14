using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Abstract.BookPhoto;
using LibraryWebAPI.Dtos.BookPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Book
{
    public class Book_GetDto : Book_Dto_Base
    {
        public Guid Id { get; set; }
        public override List<BookPhoto_Dto_Base> BookPhotos { get; set; } = new List<BookPhoto_Dto_Base>();

    }
}
