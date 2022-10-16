using LibraryWebAPI.Dtos.BookDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Parameters.Book;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// 多筆書籍
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("List")]
        public async Task<ActionResult<PagedResponse<List<Book_GetDto>>>> Get([FromBody] BookSelectParameter filter)
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

        /// <summary>
        /// 更新書籍
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromForm] Book_PutDto entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            //更新前確認Id，沒有此帳號回傳404
            if (!_bookService.Check(id))
            {
                return NotFound();
            }

            var result = await _bookService.Update(entity);
            if (!result)
            {
                return StatusCode(500, "存取發生錯誤");
            }

            //更新成功
            return NoContent();
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!_bookService.Check(id))
            {
                return NotFound();
            }

            await _bookService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// 下拉式選單書籍列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("SelectList")]
        public async Task<IActionResult> GetSelectList()
        {
            var result = await _bookService.GetSelectList();
            return Ok(result);
        }

        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        [HttpPost("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromBody] BookSelectParameter filter)
        {
            var excelData = await _bookService.ExportExcel(filter);
            return File(excelData, ContentType, "書籍列表匯出.xlsx");
        }

        [HttpPost("ImportExcel")]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            var response = await _bookService.ImportExcel(file);

            return Ok(response);
        }

  
        [HttpPost("PostBooks")]
        public async Task<ActionResult<Book_GetDto>> PostBooks([FromBody] Book_MultiplePostDto entity)
        {
            var response = await _bookService.AddMultiple(entity);

            return Ok(response);
        }
    }
}
