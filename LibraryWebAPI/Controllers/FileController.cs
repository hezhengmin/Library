using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using LibraryWebAPI.Dtos.UploadFile;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;

        public FileController(FileService fileService)
        {
            _fileService = fileService;
        }

        /// <summary>
        /// 多檔案下載，壓縮為一個zip
        /// </summary>
        /// <returns></returns>
        [HttpPost("DownloadZip")]
        public IActionResult DownloadZip([FromBody] File_PostDto entity)
        {
            var (fileType ,bytes,fileName) = _fileService.GetFilesToZip(entity.Guids);
            
            return File(bytes, fileType, fileName);
        }
    }
}
