using LibraryWebAPI.Pagination;
using System;

namespace LibraryWebAPI.Parameters.Loan
{
    public class LoanSelectParameter
    {
        /// <summary>
        /// 書的標題
        /// </summary>
        public string BookTitle { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 借出開始日期
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// 借出結束日期
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// 書籍歸還日期
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        public PaginationResult PaginationResult { get; set; } = new PaginationResult();
    }
}
