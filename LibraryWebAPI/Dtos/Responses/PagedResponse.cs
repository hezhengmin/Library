namespace LibraryWebAPI.Dtos.Responses
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        /// <summary>
        /// 總頁數
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 總筆數
        /// </summary>
        public int TotalRecords { get; set; }

        public PagedResponse()
        {

        }

        public PagedResponse(T data, int totalPages, int totalRecords)
        {
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Data = data;
        }
    }
}
