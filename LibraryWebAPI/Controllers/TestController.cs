using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
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
    }
}
