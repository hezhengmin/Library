using LibraryWebAPI.Dtos.BookPhotoDto;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookPhotoController : ControllerBase
    {
        private readonly BookPhotoService _bookPhotoService;
        private readonly BookService _bookService;

        public BookPhotoController(BookPhotoService bookPhotoService, BookService bookService)
        {
            _bookPhotoService = bookPhotoService;
            _bookService = bookService;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!_bookPhotoService.Check(id))
            {
                return NotFound();
            }

            await _bookPhotoService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// 書籍_上傳圖片
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("{bookId}")]
        public async Task<IActionResult> Post(Guid bookId, [FromForm] BookPhoto_PostDto entity)
        {
            if (!_bookService.Check(bookId))
            {
                return NotFound();
            }

            var result = await _bookPhotoService.Add(entity);

            if (!result)
            {
                return StatusCode(500, "存取發生錯誤");
            }

            return NoContent();
        }
    }
}
