using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net.Http;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OpenDataController : ControllerBase
    {
        private readonly OpenDataService _openDataService;

        public OpenDataController(OpenDataService openDataService)
        {
            _openDataService = openDataService;
        }

        /// <summary>
        /// 開放資料從json，轉到book資料表內
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOpenDataToBook()
        {
            var result = await _openDataService.GetOpenDataBook();
            if (result == null) return BadRequest("匯入有誤");
            return Ok(result);
        }
    }
}
