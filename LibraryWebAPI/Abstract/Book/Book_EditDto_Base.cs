using LibraryWebAPI.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_EditDto_Base : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        public  string Title { get; set; }

        [Required]
        public  int Status { get; set; }

        [Required]
        public  int NumberOfCopies { get; set; }

        public virtual string Isbn { get; set; }
        public virtual string Issn { get; set; }

        private string _gpn { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MinLength(10, ErrorMessage = "{0}不得低於{1}個字元")]
        public  string Gpn
        {
            get => _gpn;
            set => _gpn = value ?? "";
        }


        private string _publisher { get; set; }
        public  string Publisher
        {
            get => _publisher;
            set => _publisher = value ?? "";
        }

        private string _rightCondition { get; set; }
        public  string RightCondition
        {
            get => _rightCondition;
            set => _rightCondition = value ?? "";
        }

        private string _creator { get; set; }
        public  string Creator
        {
            get => _creator;
            set => _creator = value ?? "";
        }

        [Required]
        public  DateTime PublishDate { get; set; }

        private string _edition { get; set; }
        public  string Edition
        {
            get => _edition;
            set => _edition = value ?? "";
        }

        private string _cover { get; set; }
        public  string Cover
        {
            get => _cover;
            set => _cover = value ?? "";
        }

        private string _classify { get; set; }
        public  string Classify
        {
            get => _classify;
            set => _classify = value ?? "";
        }

        private string _gpntype { get; set; }
        public  string Gpntype
        {
            get => _gpntype;
            set => _gpntype = value ?? "";
        }

        private string _subject { get; set; }
        public  string Subject
        {
            get => _subject;
            set => _subject = value ?? "";
        }


        private string _governance { get; set; }
        public  string Governance
        {
            get => _governance;
            set => _governance = value ?? "";
        }

        private string _grade { get; set; }
        public  string Grade
        {
            get => _grade;
            set => _grade = value ?? "";
        }

        private int? _pages { get; set; }
        public  int Pages
        {
            get
            {
                return _pages.HasValue ? _pages.Value : 0;
            }

            set => _pages = value;
        }

        private string _size { get; set; }
        public  string Size
        {
            get => _size;
            set => _size = value ?? "";
        }

        private string _binding { get; set; }
        public  string Binding
        {
            get => _binding;
            set => _binding = value ?? "";
        }

        private string _language { get; set; }
        public  string Language
        {
            get => _language;
            set => _language = value ?? "";
        }

        private string _introduction { get; set; }
        public  string Introduction
        {
            get => _introduction;
            set => _introduction = value ?? "";
        }

        private string _catalog { get; set; }
        public  string Catalog
        {
            get => _catalog;
            set => _catalog = value ?? "";
        }

        private int? _price { get; set; }
        public  int Price
        {
            get
            {
                if (_price.HasValue) return _price.Value;
                else return 0;
            }
            set => _price = value;
        }

        private string _targetPeople { get; set; }
        public  string TargetPeople
        {
            get => _targetPeople;
            set => _targetPeople = value ?? "";
        }

        private string _types { get; set; }
        public  string Types
        {
            get => _types;
            set => _types = value ?? "";
        }

        private string _attachment { get; set; }
        public  string Attachment
        {
            get => _attachment;
            set => _attachment = value ?? "";
        }

        private string _url { get; set; }
        public  string Url
        {
            get => _url;
            set => _url = value ?? "";
        }

        private string _duration { get; set; }
        public  string Duration
        {
            get => _duration;
            set => _duration = value ?? "";
        }

        private string _numbers { get; set; }
        public  string Numbers
        {
            get => _numbers;
            set => _numbers = value ?? "";
        }


        private string _restriction { get; set; }
        public  string Restriction
        {
            get => _restriction;
            set => _restriction = value ?? "";
        }

        /// <summary>
        /// 停刊註記
        /// </summary>
        public DateTime? CeasedDate { get; set; }
        private string _authority { get; set; }
        public  string Authority
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

            if(dto.Id != Guid.Empty)
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
