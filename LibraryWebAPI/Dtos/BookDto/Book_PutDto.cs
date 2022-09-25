using LibraryWebAPI.Abstract.Book;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.BookDto
{
    public class Book_PutDto : Book_Dto_Base
    {
        [Required]
        public override string Title { get; set; }

        [Required]
        public override int Status { get; set; }

        [Required]
        public override int NumberOfCopies { get; set; }

        private string _gpn { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MinLength(10, ErrorMessage = "{0}不得低於{1}個字元")]
        public override string Gpn
        {
            get => _gpn;
            set => _gpn = value ?? "";
        }


        private string _publisher { get; set; }
        public override string Publisher
        {
            get => _publisher;
            set => _publisher = value ?? "";
        }

        private string _rightCondition { get; set; }
        public override string RightCondition
        {
            get => _rightCondition;
            set => _rightCondition = value ?? "";
        }

        private string _creator { get; set; }
        public override string Creator
        {
            get => _creator;
            set => _creator = value ?? "";
        }

        private string _publishDate { get; set; }
        public override string PublishDate
        {
            get => _publishDate;
            set => _publishDate = value ?? "";
        }
        private string _edition { get; set; }
        public override string Edition
        {
            get => _edition;
            set => _edition = value ?? "";
        }

        private string _cover { get; set; }
        public override string Cover
        {
            get => _cover;
            set => _cover = value ?? "";
        }

        private string _classify { get; set; }
        public override string Classify
        {
            get => _classify;
            set => _classify = value ?? "";
        }

        private string _gpntype { get; set; }
        public override string Gpntype
        {
            get => _gpntype;
            set => _gpntype = value ?? "";
        }

        private string _subject { get; set; }
        public override string Subject
        {
            get => _subject;
            set => _subject = value ?? "";
        }


        private string _governance { get; set; }
        public override string Governance
        {
            get => _governance;
            set => _governance = value ?? "";
        }

        private string _grade { get; set; }
        public override string Grade
        {
            get => _grade;
            set => _grade = value ?? "";
        }

        private int? _pages { get; set; }
        public override int Pages
        {
            get
            {
                return _pages.HasValue ? _pages.Value : 0;
            }

            set => _pages = value;
        }

        private string _size { get; set; }
        public override string Size
        {
            get => _size;
            set => _size = value ?? "";
        }

        private string _binding { get; set; }
        public override string Binding
        {
            get => _binding;
            set => _binding = value ?? "";
        }

        private string _language { get; set; }
        public override string Language
        {
            get => _language;
            set => _language = value ?? "";
        }

        private string _introduction { get; set; }
        public override string Introduction
        {
            get => _introduction;
            set => _introduction = value ?? "";
        }

        private string _catalog { get; set; }
        public override string Catalog
        {
            get => _catalog;
            set => _catalog = value ?? "";
        }

        private int? _price { get; set; }
        public override int Price
        {
            get
            {
                if (_price.HasValue) return _price.Value;
                else return 0;
            }
            set => _price = value;
        }

        private string _targetPeople { get; set; }
        public override string TargetPeople
        {
            get => _targetPeople;
            set => _targetPeople = value ?? "";
        }

        private string _types { get; set; }
        public override string Types
        {
            get => _types;
            set => _types = value ?? "";
        }

        private string _attachment { get; set; }
        public override string Attachment
        {
            get => _attachment;
            set => _attachment = value ?? "";
        }

        private string _url { get; set; }
        public override string Url
        {
            get => _url;
            set => _url = value ?? "";
        }

        private string _duration { get; set; }
        public override string Duration
        {
            get => _duration;
            set => _duration = value ?? "";
        }

        private string _numbers { get; set; }
        public override string Numbers
        {
            get => _numbers;
            set => _numbers = value ?? "";
        }


        private string _restriction { get; set; }
        public override string Restriction
        {
            get => _restriction;
            set => _restriction = value ?? "";
        }


        private string _authority { get; set; }
        public override string Authority
        {
            get => _authority;
            set => _authority = value ?? "";
        }

        /// <summary>
        /// 多檔或單一檔案
        /// </summary>
        public ICollection<IFormFile> Files { get; set; }
    }
}
