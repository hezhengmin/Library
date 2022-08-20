using LibraryWebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebAPI.Parameters.Book
{
    public class BookSelectParameter
    {
        /// <summary>
        /// 書的標題
        /// </summary>
        public string Title { get; set; }
        public string Isbn { get; set; }
        /// <summary>
        /// 版次
        /// </summary>
        public string Edition { get; set; }
        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime? CreateAt { get; set; }
        public PaginationFilter PaginationFilter { get; set; }
    }
}
