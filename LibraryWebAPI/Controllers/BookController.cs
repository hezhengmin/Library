using LibraryWebAPI.Dtos.Book;
using LibraryWebAPI.Parameters.Book;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly UploadFileService _uploadFileService;

        public BookController(BookService bookService, UploadFileService uploadFileService)
        {
            _bookService = bookService;
            _uploadFileService = uploadFileService;
        }

        /// <summary>
        /// 多筆書籍
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Book_GetDto>>> Get([FromBody] BookSelectParameter filter)
        {
            return await _bookService.Get(filter);
        }

        /// <summary>
        /// 單筆書籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book_GetDto>> Get([FromRoute] Guid id)
        {
            var result = await _bookService.GetDto(id);
            if (result == null) return NotFound();
            return result;
        }

        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Book_GetDto>> Post([FromForm] Book_PostDto entity)
        {
            var book = await _bookService.Add(entity);
            if (book == null) return BadRequest("新增失敗");

            var result = await _bookService.GetDto(book.Id);

            return CreatedAtAction(nameof(Get), new { id = book.Id }, result);
        }
    }
}
