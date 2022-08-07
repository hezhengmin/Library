using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zheng.Application.Parameters.Book
{
    public class BookSelectParameter
    {
        /// <summary>
        /// 書的標題
        /// </summary>
        public string title { get; set; }
        public DateTime? createAt { get; set; }
    }
}
