using LibraryWebAPI.Dtos.LoanDto;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan_GetDto>> Get([FromRoute] Guid id)
        {
            var result = await _loanService.GetDto(id);
            if (result == null) return NotFound();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Loan_GetDto>> Post([FromForm] Loan_PostDto entity)
        {
            var loan = await _loanService.Add(entity);
            if (loan == null) return BadRequest("新增失敗");

            var result = await _loanService.GetDto(loan.Id);

            return CreatedAtAction(nameof(Get), new { id = loan.Id }, result);
        }
    }
}
