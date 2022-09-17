using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryWebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookPhotoController : ControllerBase
    {
        private readonly BookPhotoService _bookPhotoService;

        public BookPhotoController(BookPhotoService bookPhotoService)
        {
            _bookPhotoService = bookPhotoService;
        }

     
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!_bookPhotoService.Check(id))
            {
                return NotFound();
            }

            await _bookPhotoService.Delete(id);

            return NoContent();
        }
    }
}
