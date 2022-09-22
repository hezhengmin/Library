using LibraryWebAPI.Abstract.BookPhoto;

namespace LibraryWebAPI.Dtos.BookPhotoDto
{
    public class BookPhoto_GetDto : BookPhoto_Dto_Base
    {
        /// <summary>
        /// 完整檔名
        /// </summary>
        public string FileCompleteName { get; set; }
    }
}
