using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zheng.Infrastructure.Data;
using Zheng.Infrastructure.Models;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public AccountController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return _context.Account;
        }

        [HttpGet("{id}")]
        public ActionResult<Account> Get(Guid id)
        {
            var result = _context.Account.Find(id);
            /*
             exec sp_executesql N'SELECT TOP(1) [a].[Id], [a].[AccountId], [a].[Password], [a][SystemDate]
             FROM [Account] AS [a]
             WHERE [a].[Id] = @__p_0',N'@__p_0 uniqueidentifier',@__p_0='6B568F00-ECD6-4C69-94C9-C812DC574B98'
             */
            if (result == null) return NotFound("找不到帳號");

            return result;
        }

        [HttpPost]
        public ActionResult<Account> Post([FromBody] Account entity)
        {
            _context.Account.Add(entity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { Id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
