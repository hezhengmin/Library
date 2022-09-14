using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly UploadFileService _uploadFileService;

        public DownloadController(UploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var entity = await _uploadFileService.Get(id);

            if (entity == null)
                return NotFound(); 

            var stream = await _uploadFileService.GetUploadFileStream(id);

            return File(stream, entity.ContentType, $"{entity.Name}{entity.Extension}");
        }
    }
}
