using LibraryWebAPI.Dtos.AccountDto;
using LibraryWebAPI.Dtos.InputModel;
using LibraryWebAPI.Helpers;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zheng.Infra.Data.Models;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService, JwtHelper jwtHelper)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 多筆帳號
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<Account_GetDto>>> Get()
        {
            return await _accountService.Get();
        }

        /// <summary>
        /// 單筆帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<Account_GetDto>> Get(Guid id)
        {
            var result = await _accountService.GetDto(id);

            if (result == null) return NotFound("找不到帳號");

            return Ok(result);
        }

        /// <summary>
        /// 新增/註冊帳號
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] Account_PostDto entity)
        {
            var regResponse = await _accountService.Add(entity);

            return Ok(regResponse);
        }

        /// <summary>
        /// 更新帳密
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
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
        /// 部分更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Account> patchEntity)
        {
            //https://docs.microsoft.com/zh-tw/aspnet/core/web-api/jsonpatch?view=aspnetcore-5.0
            var result = await _accountService.Update(id, patchEntity);
            if (!result) return BadRequest("更新失敗");
            //更新成功
            return NoContent();
        }


        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
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
        /// 登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromBody] Account_LoginDto entity)
        {
            var response = await _accountService.Login(entity);

            return Ok(response);
        }

        /// <summary>
        /// 重設密碼
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("ResetPassword")]
        [Produces("application/json")]
        public async Task<IActionResult> ResetPassword([FromBody] Account_ResetPasswordDto entity)
        {

            var result = await _accountService.UpdatePassword(entity);
            if (!result)
            {
                return StatusCode(500, "存取發生錯誤");
            }

            //更新成功
            return NoContent();
        }

        /// <summary>
        /// 忘記密碼
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        [Produces("application/json")]
        public async Task<IActionResult> ForgetPassword([FromBody] Account_ForgetPasswordDto entity)
        {
            var response = await _accountService.ForgetPassword(entity);

            return Ok(response);
        }

        /// <summary>
        /// 帳號清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("SelectList")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSelectList()
        {
            var result = await _accountService.GetSelectList();
            return Ok(result);
        }

        /// <summary>
        /// 從過期的 jwtToken 取得新的 token
        /// </summary>
        /// <param name="tokenInputModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GetNewToken")]
        [Produces("application/json")]
        public async Task<IActionResult> GetNewToken(TokenInputModel tokenInputModel)
        {
            try
            {
                var result = await _accountService.GetNewToken(tokenInputModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest( $"取得新的AccessToken發生錯誤：{ex.Message}");
            }
        }
    }
}
