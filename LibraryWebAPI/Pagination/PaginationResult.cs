﻿namespace LibraryWebAPI.Pagination
{
    public class PaginationResult
    {
        private int _pageNumber { get; set; }
        /// <summary>
        /// 第幾頁
        /// </summary>
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value < 1 ? 1 : value;
            }
        }
        private int _pageSize { get; set; }
        /// <summary>
        /// 每頁幾筆
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value < 10 ? 10 : value;
            }
        }

        public PaginationResult()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationResult(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
