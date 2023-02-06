using System;

namespace LibraryWebAPI.Dtos.UploadFile
{
    public class UploadFile_AddDto
    {
        /// <summary>
        /// 系統編號
        /// </summary>
        public Guid Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 副檔名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 檔案類型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 檔案大小
        /// </summary>
        public long Length { get; set; }
    }
}
