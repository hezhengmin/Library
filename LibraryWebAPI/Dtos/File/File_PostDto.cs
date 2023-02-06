using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Dtos.UploadFile
{
    /// <summary>
    /// 多檔案 guids
    /// </summary>
    public class File_PostDto
    {
        /// <summary>
        /// 系統編號
        /// </summary>
        public List<string> Guids { get; set; }
    }
}
