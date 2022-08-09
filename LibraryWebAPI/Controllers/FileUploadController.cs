using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }


        /// <summary>
        /// 單一檔案上傳
        /// </summary>
        /// <param name="singleFile"></param>
        [HttpPost("SinglePost")]
        public void Post(IFormFile singleFile)
        {
            var rootPath = _env.ContentRootPath + "\\Uploads\\";

            //路徑如果不存在，創建起來
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            if (singleFile.Length > 0)
            {
                var filePath = singleFile.FileName;
                using (var stream = System.IO.File.Create(rootPath + filePath))
                {
                    singleFile.CopyTo(stream);
                }
            }
        }


        /// <summary>
        /// 多檔上傳
        /// </summary>
        /// <param name="files"></param>
        [HttpPost]
        public void Post(ICollection<IFormFile> files)
        {
            /*
            多檔格式
            IFormFileCollection
            IEnumerable <IFormFile>
            List<IFormFile>
            ICollection<IFormFile>
            */
            var rootPath = _env.ContentRootPath + "\\Uploads\\";

            //路徑如果不存在，創建起來
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = file.FileName;
                    using (var stream = System.IO.File.Create(rootPath + filePath))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
        }
    }
}
