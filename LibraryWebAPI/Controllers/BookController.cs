using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zheng.Application.Services;
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
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await _bookService.Get();
        }

        /// <summary>
        /// Get 加上篩選條件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="createAt"></param>
        /// <returns></returns>
        [HttpGet("GetFilter")]
        public async Task<ActionResult<List<Book>>> Get(string title, DateTime? createAt)
        {
            return await _bookService.Get(title, createAt);
        }
    }
}
