using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.Responses
{
    public abstract class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<string>();
        }
        public bool Success { get; set; }
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
