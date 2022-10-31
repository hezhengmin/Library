using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Zheng.Infrastructure.Models.Book;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_EditDto_Base : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "書名不能超過{1}個字元")]
        public string Title { get; set; }

        [Required]
        [EnumDataType(typeof(StatusType), ErrorMessage = "超過列舉範圍值")]
        public int Status { get; set; }

        [Required]
        public int NumberOfCopies { get; set; }

        [StringLength(15, ErrorMessage = "{0}不得超過{1}個字元")]
        public virtual string Isbn { get; set; }

        [StringLength(8, ErrorMessage = "{0}不得超過{1}個字元")]
        public virtual string Issn { get; set; }

        private string _gpn { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MinLength(10, ErrorMessage = "{0}不得低於{1}個字元")]
        [MaxLength(10, ErrorMessage = "{0}不得超過{1}個字元")]
        public string Gpn
        {
            get => _gpn;
            set => _gpn = value ?? "";
        }

        private string _publisher { get; set; }

        [StringLength(50, ErrorMessage = "出版單位不得超過{1}個字元")]
        public string Publisher
        {
            get => _publisher;
            set => _publisher = value ?? "";
        }

        private string _rightCondition { get; set; }

        [StringLength(50, ErrorMessage = "出版情況不得超過{1}個字元")]
        public string RightCondition
        {
            get => _rightCondition;
            set => _rightCondition = value ?? "";
        }

        private string _creator { get; set; }

        [StringLength(500, ErrorMessage = "作者資訊不得超過{1}個字元")]
        public string Creator
        {
            get => _creator;
            set => _creator = value ?? "";
        }

        [Required(ErrorMessage = "出版日期必填")]
        public DateTime PublishDate { get; set; }

        private string _edition { get; set; }

        [StringLength(50, ErrorMessage = "版次不得超過{1}個字元")]
        public string Edition
        {
            get => _edition;
            set => _edition = value ?? "";
        }

        private string _cover { get; set; }
        [StringLength(200, ErrorMessage = "書封連結不得超過{1}個字元")]
        public string Cover
        {
            get => _cover;
            set => _cover = value ?? "";
        }

        private string _classify { get; set; }
        
        [StringLength(50, ErrorMessage = "書籍分類不得超過{1}個字元")]
        public string Classify
        {
            get => _classify;
            set => _classify = value ?? "";
        }

        private string _gpntype { get; set; }

        [Required(ErrorMessage = "出版品分類必填")]
        [StringLength(50, ErrorMessage = "出版品分類，不得超過{1}個字元")]
        public string Gpntype
        {
            get => _gpntype;
            set => _gpntype = value ?? "";
        }

        private string _subject { get; set; }
        [StringLength(50, ErrorMessage = "主題分類，不得超過{1}個字元")]
        public string Subject
        {
            get => _subject;
            set => _subject = value ?? "";
        }

        private string _governance { get; set; }
        [StringLength(50, ErrorMessage = "施政分類，不得超過{1}個字元")]
        public string Governance
        {
            get => _governance;
            set => _governance = value ?? "";
        }

        private string _grade { get; set; }
        [StringLength(10, ErrorMessage = "級別，不得超過{1}個字元")]
        public string Grade
        {
            get => _grade;
            set => _grade = value ?? "";
        }

        private int? _pages { get; set; }

        [Required(ErrorMessage = "頁數必填")]
        [RegularExpression(@"^(0|[1-9][0-9]*)$",ErrorMessage = "只能輸入零和非零開頭的數字")]
        public int Pages
        {
            get
            {
                return _pages.HasValue ? _pages.Value : 0;
            }

            set => _pages = value;
        }

        private string _size { get; set; }
        [StringLength(50, ErrorMessage = "開數，不得超過{1}個字元")]
        public string Size
        {
            get => _size;
            set => _size = value ?? "";
        }

        private string _binding { get; set; }
        [StringLength(10, ErrorMessage = "裝訂，不得超過{1}個字元")]
        public string Binding
        {
            get => _binding;
            set => _binding = value ?? "";
        }

        private string _language { get; set; }
        [Required(ErrorMessage = "語言必填")]
        [StringLength(10, ErrorMessage = "語言，不得超過{1}個字元")]
        public string Language
        {
            get => _language;
            set => _language = value ?? "";
        }

        private string _introduction { get; set; }
        [StringLength(2000, ErrorMessage = "書籍介紹，不得超過{1}個字元")]
        public string Introduction
        {
            get => _introduction;
            set => _introduction = value ?? "";
        }

        private string _catalog { get; set; }
        [StringLength(3000, ErrorMessage = "目次，不得超過{1}個字元")]
        public string Catalog
        {
            get => _catalog;
            set => _catalog = value ?? "";
        }

        private int? _price { get; set; }

        [Required(ErrorMessage = "價格必填")]
        [RegularExpression(@"^(0|[1-9][0-9]*)$", ErrorMessage = "只能輸入零和非零開頭的數字")]
        public int Price
        {
            get
            {
                if (_price.HasValue) return _price.Value;
                else return 0;
            }
            set => _price = value;
        }

        private string _targetPeople { get; set; }

        [StringLength(50, ErrorMessage = "適用對象，不得超過{1}個字元")]
        public string TargetPeople
        {
            get => _targetPeople;
            set => _targetPeople = value ?? "";
        }

        private string _types { get; set; }

        [StringLength(50, ErrorMessage = "資料類型，不得超過{1}個字元")]
        public string Types
        {
            get => _types;
            set => _types = value ?? "";
        }

        private string _attachment { get; set; }

        [Required(ErrorMessage = "附件必填")]
        [StringLength(50, ErrorMessage = "附件，不得超過{1}個字元")]
        public string Attachment
        {
            get => _attachment;
            set => _attachment = value ?? "";
        }

        private string _url { get; set; }

        [StringLength(1000, ErrorMessage = "出版品網址-線上版或試閱版，不得超過{1}個字元")]
        public string Url
        {
            get => _url;
            set => _url = value ?? "";
        }

        private string _duration { get; set; }

        [Required(ErrorMessage = "播放時間長度必填")]
        [StringLength(50, ErrorMessage = "播放時間長度，不得超過{1}個字元")]
        public string Duration
        {
            get => _duration;
            set => _duration = value ?? "";
        }

        private string _numbers { get; set; }

        [StringLength(50, ErrorMessage = "字號，不得超過{1}個字元")]
        public string Numbers
        {
            get => _numbers;
            set => _numbers = value ?? "";
        }


        private string _restriction { get; set; }
        [StringLength(10, ErrorMessage = "權利範圍，不得超過{1}個字元")]
        public string Restriction
        {
            get => _restriction;
            set => _restriction = value ?? "";
        }

        /// <summary>
        /// 停刊註記
        /// </summary>
        public DateTime? CeasedDate { get; set; }
        private string _authority { get; set; }

        [Required(ErrorMessage = "授權資訊必填")]
        [StringLength(500, ErrorMessage = "授權資訊，不得超過{1}個字元")]
        public string Authority
        {
            get => _authority;
            set => _authority = value ?? "";
        }

        /// <summary>
        /// 多檔或單一檔案
        /// </summary>
        public ICollection<IFormFile> Files { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            BookService _bookService = (BookService)validationContext.GetService(typeof(BookService));

            var dto = (Book_EditDto_Base)validationContext.ObjectInstance;

            //編輯才要驗證
            if (dto.Id != Guid.Empty)
            {
                var leastNumberOfCopiesCount = _bookService.GetLeastNumberOfCopiesCount(dto.Id).Result;

                if (leastNumberOfCopiesCount > dto.NumberOfCopies)
                {
                    yield return new ValidationResult($"庫存數量不能低於{leastNumberOfCopiesCount}", new[] { "NumberOfCopies" });
                }
            }
        }
    }
}
