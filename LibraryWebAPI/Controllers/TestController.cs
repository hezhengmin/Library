using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using Serilog.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zheng.Infra.Data.Data;
using Zheng.Utilities.Cryptography;
using static LibraryWebAPI.Controllers.TestController;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<TestController> _logger;

        public TestController(LibraryDbContext context, ILogger<TestController> logger)
        {
            _context = context;
            _logger = logger;
        }

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

        /// <summary>
        /// 日誌記錄檔 Serilog Log 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetLog")]
        public IActionResult GetLog()
        {
            /* 參考寫法
            https://www.youtube.com/watch?v=IDsiVeOe6Uw
            https://github.com/serilog/serilog-aspnetcore
            https://github.com/Nehanthworld/Asp.Net-Core-Web-API-Tutorial
            */
            _logger.LogInformation("Hello, world!");
            _logger.LogTrace("Log message from trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            return Ok();
        }

        [HttpGet("GetLeftJoin")]
        public IActionResult GetLeftJoin()
        {
            #region SQL
            /*
            SELECT [b].[Id] AS [BookId], [b].[Title] AS [BookTitle], [b0].[UploadFileId]
            FROM [Book] AS [b]
            LEFT JOIN [BookPhoto] AS [b0] ON [b].[Id] = [b0].[BookId]
            */
            #endregion
            var result = (from b in _context.Books
                          join bp in _context.BookPhotos
                          on b.Id equals bp.BookId into g
                          from bp in g.DefaultIfEmpty()
                          select new
                          {
                              BookId = b.Id,
                              BookTitle = b.Title,
                              UploadFileId = bp.UploadFileId != null ? bp.UploadFileId : (Guid?)null
                          }
                ).ToList();

            return Ok(result);
        }

        /// <summary>
        /// 多個單詞的字串陣列，將每個單詞拆分成字母
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSelectMany")]
        public IActionResult GetSelectMany()
        {
            //結果 {"h", "e", "l", "l", "o", "w", "o", "r", "l", "d"}
            string[] words = { "hello", "world" };
            char[] letters = words.SelectMany(word => word).ToArray();

            return Ok(letters);
        }


        [HttpGet("GetSelectMany2")]
        public IActionResult GetSelectMany2()
        {
            var courses = new List<Course>()
            {
                new Course() { CourseId = 1, StudentId = 1,  CourseName = "國文" },
                new Course() { CourseId = 2, StudentId = 1,  CourseName = "英文" },
                new Course() { CourseId = 3, StudentId = 3,  CourseName = "數學" },
                new Course() { CourseId = 4, StudentId = 3,  CourseName = "英文" },

            };

            var students = new List<Student>()
            {
                new Student(){ StudentId = 1 ,Name = "Amy"},
                new Student(){ StudentId = 2 ,Name = "Neci"},
                new Student(){ StudentId = 3 ,Name = "Tommy"},
            };

            var result = students.GroupJoin(courses,
                                   s => s.StudentId,
                                   c => c.StudentId,
                                   (student, course) => new
                                   {
                                       StudentName = student.Name,
                                       Course = course
                                   })
                                 .SelectMany(x => x.Course.DefaultIfEmpty(),
                                   (student, course) => new
                                   {
                                       StudentName = student.StudentName,
                                       CourseId = course?.CourseId,
                                       CourseName = course?.CourseName
                                   }).ToList();

            return Ok(result);
        }

        public class Student
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
        }

        public class Course
        {
            public int CourseId { get; set; }
            public int StudentId { get; set; }
            public string CourseName { get; set; }
        }
    }
}
