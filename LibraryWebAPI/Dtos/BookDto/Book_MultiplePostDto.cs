using LibraryWebAPI.Abstract.Book;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.BookDto
{
    public class Book_MultiplePostDto 
    {
        public List<Book_PostDto> Books { get; set; } 
    }
}
