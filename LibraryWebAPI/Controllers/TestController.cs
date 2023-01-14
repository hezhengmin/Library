using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using Serilog.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using Zheng.Utilities.Cryptography;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public class TestDto
        {
            public int A { get; set; }
            public int B { get; set; }
        }

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public dynamic Get([FromRoute] string id,
            [FromQuery] string query,
            [FromBody] TestDto testDto)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(id);
            list.Add(query);
            list.Add(testDto);

            return list;
        }


        [HttpGet("FormTest")]
        public dynamic Get([FromQuery] string query,
            [FromForm] string form)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(query);
            list.Add(form);

            return list;
        }


        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        /// <summary>
        /// Create and download workbook 匯出範例(EPPlus)
        /// </summary>
        /// <returns></returns>
        [HttpGet("ExportExcel")]
        public IActionResult WebSample1()
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Sheet 1");
                sheet.Cells["A1:C1"].Merge = true;
                sheet.Cells["A1"].Style.Font.Size = 18f;
                sheet.Cells["A1"].Style.Font.Bold = true;
                sheet.Cells["A1"].Value = "匯出範例(EPPlus)";
                var excelData = package.GetAsByteArray();
                var fileName = "MyWorkbook.xlsx";
                return File(excelData, ContentType, fileName);
            }
        }

        /// <summary>
        /// 輸入明文取得MD5字串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        [HttpGet("GetMD5/{source}")]
        public IActionResult GetMD5(string source)
        {
            string result = MD5Extensions.EncryptByte(source);
            return Ok(result);
        }

        /// <summary>
        /// 日誌記錄檔 Serilog Log 
        /// <para>https://www.youtube.com/watch?v=IDsiVeOe6Uw</para>
        /// <para>https://github.com/serilog/serilog-aspnetcore</para>
        /// <para>https://github.com/Nehanthworld/Asp.Net-Core-Web-API-Tutorial</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLog")]
        public IActionResult GetLog()
        {
            _logger.LogInformation("Hello, world!");
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            return Ok();
        }
    }
}
