using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryWebAPI.Dtos.Book;
using LibraryWebAPI.Parameters.Book;
using LibraryWebAPI.Services;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get([FromQuery] BookSelectParameter filter)
        {
            return await _bookService.Get(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get([FromRoute] Guid id)
        {
            return await _bookService.Get(id);
        }

        [HttpGet("GetDto/{id}")]
        public async Task<ActionResult<Book_GetDto>> GetDto([FromRoute] Guid id)
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
        public async Task<ActionResult<Book_GetDto>> Post([FromBody] Book_PostDto entity)
        {
            var book = await _bookService.Add(entity);
            if (book == null) return BadRequest("新增失敗");

            var result = await _bookService.GetDto(book.Id);

            return CreatedAtAction(nameof(GetDto), new { id = book.Id }, result);
        }
    }
}
