using LibraryWebAPI.Abstract.Book;
using LibraryWebAPI.Abstract.BookPhoto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Book
{
    public class Book_PostDto : Book_Dto_Base
    {
        public override List<BookPhoto_Dto_Base> BookPhotos { get; set; } = new List<BookPhoto_Dto_Base>();
    }
}
