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
        /// 回應訊息(例如:資料異動成功)
        /// </summary>
        public string Message { get; set; } = string.Empty;
        
        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public List<string> Errors { get; set; }
    }
}
