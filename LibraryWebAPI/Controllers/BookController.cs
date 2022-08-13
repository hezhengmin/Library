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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookController(BookService bookService, IHttpContextAccessor httpContextAccessor)
        {
            _bookService = bookService;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book_AddDto entity)
        {

            var book = await _bookService.Add(entity);
            if (book == null) return BadRequest("新增失敗");

            return CreatedAtAction(nameof(Get), new { Id = book.Id }, book);
        }
    }
}
