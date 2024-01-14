using LibraryWebAPI.Dtos.ValidateCodeDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Zheng.Utilities.Cryptography;
using Zheng.Utilities.Helpers;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateCodeController : ControllerBase
    {
        [HttpGet("GetValidateCode")]
        public IActionResult GetValidateCode()
        {
            string codeValue = "";
            var codeImg = ValidateCodeHelper.CreateImage(out codeValue, 6);
            //codeValue = codeValue.ToUpper();//验证码不分大小写  

            ValidateCode_GetDto dto = new ValidateCode_GetDto()
            {
                Base64 = "data:image/jpg;base64," + Convert.ToBase64String(codeImg),
                Hash = MD5Extensions.EncryptByte(codeValue),
            };

            return Ok(dto);
        }
    }
}
