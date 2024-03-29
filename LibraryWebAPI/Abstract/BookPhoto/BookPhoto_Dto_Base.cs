﻿using System;

namespace LibraryWebAPI.Abstract.BookPhoto
{
    public class BookPhoto_Dto_Base
    {
        public Guid Id { get; set; }
        public Guid UploadFileId { get; set; }
        /// <summary>
        /// 檔名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附檔名
        /// </summary>
        public string Extension { get; set; }
       
    }
}
