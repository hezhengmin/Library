using LibraryWebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryWebAPI.Services;
using LibraryWebAPI.Dtos.Account;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly JwtHelper _jwtHelper;

        public AccountController(AccountService accountService, JwtHelper jwtHelper)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account_GetDto>>> Get()
        {
            return await _accountService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account_GetDto>> Get(Guid id)
        {
            var result = await _accountService.GetDto(id);

            if (result == null) return NotFound("找不到帳號");

            return Ok(result);
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] Account_PostDto entity)
        {
            var account = await _accountService.Add(entity);

            if (account == null) return BadRequest("新增失敗");

            var result = await _accountService.GetDto(account.Id);

            return CreatedAtAction(nameof(Get), new { account.Id }, result);
        }

        /// <summary>
        /// 更新帳密
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] Account_PutDto entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            //更新前確認Id，沒有此帳號回傳404
            if (!_accountService.Check(id))
            {
                return NotFound();
            }

            var result = await _accountService.Update(entity);
            if (!result)
            {
                return StatusCode(500, "存取發生錯誤");
            }

            //更新成功
            return NoContent();
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!_accountService.Check(id))
            {
                return NotFound();
            }

            await _accountService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// 首頁登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Account_LoginDto entity)
        {
            var account = await _accountService.Login(entity);

            //登入成功
            if (account != null)
            {
                var token = _jwtHelper.GenerateJwtToken(account);

                return Ok(new { jwtToken = token });
            }
            else
            {
                return NotFound("帳號或密碼錯誤");
            }
        }
    }
}
