using LibraryWebAPI.Wappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Zheng.Application.Services;
using Zheng.Application.ViewModels.Account;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly ISessionWapper _sessionWapper;

        public AccountController(AccountService accountService, ISessionWapper sessionWapper)
        {
            _accountService = accountService;
            _sessionWapper = sessionWapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return await _accountService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> Get(Guid id)
        {
            var result = await _accountService.Get(id);

            #region SQL 查詢語法
            /*
           exec sp_executesql N'SELECT TOP(1) [a].[Id], [a].[AccountId], [a].[Password], [a][SystemDate]
           FROM [Account] AS [a]
           WHERE [a].[Id] = @__p_0',N'@__p_0 uniqueidentifier',@__p_0='6B568F00-ECD6-4C69-94C9-C812DC574B98'
           */
            #endregion

            if (result == null) return NotFound("找不到帳號");

            return result;
        }

        [HttpPost]
        public ActionResult<Account> Post([FromBody] Account_AddVM entity)
        {
            Account account = new Account();
            var result = _accountService.Add(entity, out account);

            return CreatedAtAction(nameof(Get), new { Id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Account_UpdateVM entity)
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

            if (!_accountService.Update(entity))
            {
                return StatusCode(500, "存取發生錯誤");
            }

            //更新成功
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!_accountService.Check(id))
            {
                return NotFound();
            }

            _accountService.Remove(id);

            return NoContent();
        }

        /// <summary>
        /// 首頁登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] Account_SignInVM entity)
        {

            //Session有物件，已經登入過
            if (_sessionWapper.SingInAccount != null)
            {
                return new OkObjectResult(new { entity.AccountId, Message = "已經登入" });
            }
            else
            {
                var result = _accountService.SingIn(entity);

                //登入成功
                if (result)
                {
                    _sessionWapper.SingInAccount = entity;
                    return new OkObjectResult(new { entity.AccountId, Message = "登入成功" });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
            }

        }



    }
}
