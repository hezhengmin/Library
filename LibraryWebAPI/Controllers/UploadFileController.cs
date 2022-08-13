using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly UploadFileService _uploadFileService;

        public UploadFileController(UploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        /// <summary>
        /// 單一檔案上傳
        /// </summary>
        /// <param name="singleFile"></param>
        [HttpPost("SinglePost")]
        public IActionResult Post(IFormFile singleFile)
        {
            var result = _uploadFileService.Add(singleFile).Result;
            if (!result) return BadRequest();

            //新增成功
            return NoContent();
        }

        /// <summary>
        /// 多檔上傳
        /// </summary>
        /// <param name="files"></param>
        [HttpPost]
        public IActionResult Post(ICollection<IFormFile> files)
        {
            var result = _uploadFileService.AddMultiple(files).Result;

            if (!result) return BadRequest();
            //新增成功
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _uploadFileService.Check(id))
            {
                return NotFound();
            }

            var result = await _uploadFileService.Delete(id);

            if (!result) return BadRequest();

            return NoContent();
        }
    }
}
