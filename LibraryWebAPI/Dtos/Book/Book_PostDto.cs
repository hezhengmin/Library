﻿using LibraryWebAPI.Abstract.Book;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LibraryWebAPI.Dtos.Book
{
    public class Book_PostDto : Book_Dto_Base
    {
        [Required(ErrorMessage = "{0}必填")]
        [MinLength(13, ErrorMessage = "{0}不得低於{1}個字元")]
        public override string Isbn { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [MinLength(10, ErrorMessage = "{0}不得低於{1}個字元")]
        public override string Gpn { get; set; }

        private string _issn { get; set; }
        public override string Issn
        {
            get
            {
                return _issn;
            }
            set
            {
                _issn = value ?? "";
            }
        }

        private string _grade { get; set; }
        public override string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value ?? "";
            }
        }
        private string _catalog { get; set; }
        public override string Catalog
        {
            get
            {
                return _catalog;
            }
            set
            {
                _catalog = value ?? "";
            }
        }

        private string _types { get; set; }
        public override string Types
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value ?? "";
            }
        }

        private string _url { get; set; }
        public override string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value ?? "";
            }
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
        
        /// <summary>
        /// 多檔或單一檔案
        /// </summary>
        public ICollection<IFormFile> Files { get; set; }
    }
}
