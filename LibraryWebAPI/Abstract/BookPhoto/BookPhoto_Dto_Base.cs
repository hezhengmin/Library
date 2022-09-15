using System;

namespace LibraryWebAPI.Abstract.BookPhoto
{
    public class BookPhoto_Dto_Base
    {
        public Guid UploadFileId { get; set; }
        /// <summary>
        /// 檔名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附檔名
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 完整檔名
        /// </summary>
        public string FileCompleteName { get; set; }
    }
}
