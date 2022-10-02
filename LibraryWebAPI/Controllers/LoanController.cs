using LibraryWebAPI.Dtos.LoanDto;
using LibraryWebAPI.Dtos.Responses;
using LibraryWebAPI.Parameters.Loan;
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
    public class LoanController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoanController(LoanService LoanService)
        {
            _loanService = LoanService;
        }

        /// <summary>
        /// 借閱列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("List")]
        public async Task<ActionResult<PagedResponse<List<Loan_GetDto>>>> Get([FromBody] LoanSelectParameter filter)
        {
            return await _loanService.Get(filter);
        }

        /// <summary>
        /// 取得單筆借閱
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Loan_GetDto>> Get([FromRoute] Guid id)
        {
            var result = await _loanService.GetDto(id);
            if (result == null) return NotFound();
            return result;
        }

        /// <summary>
        /// 新增借閱
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Loan_GetDto>> Post([FromBody] Loan_PostDto entity)
        {
            var loan = await _loanService.Add(entity);
            if (loan == null) return BadRequest("新增失敗");

            var result = await _loanService.GetDto(loan.Id);

            return CreatedAtAction(nameof(Get), new { id = loan.Id }, result);
        }

        /// <summary>
        /// 更新借閱
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] Loan_PutDto entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            //更新前確認Id，沒有此id回傳404
            if (!_loanService.Check(id))
            {
                return NotFound();
            }

            var result = await _loanService.Update(entity);
            if (!result)
            {
                return StatusCode(500, "存取發生錯誤");
            }

            //更新成功
            return NoContent();
        }


        /// <summary>
        /// 刪除借閱
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!_loanService.Check(id))
            {
                return NotFound();
            }

            await _loanService.Delete(id);

            return NoContent();
        }
    }
}
