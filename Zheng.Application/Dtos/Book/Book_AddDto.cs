using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zheng.Application.Dtos.Book
{
    public class Book_AddDto
    {
        /// <summary>
        /// 書名標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        public string Isbn { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public int Status { get; set; }
    }
}
