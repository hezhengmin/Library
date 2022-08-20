using LibraryWebAPI.Abstract.BookPhoto;
using System;
using System.Collections.Generic;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_Dto_Base
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string Isbn { get; set; }
        public string Issn { get; set; }
        public string Gpn { get; set; }
        public string Publisher { get; set; }
        public string RightCondition { get; set; }
        public string Creator { get; set; }
        public string PublishDate { get; set; }
        public string Edition { get; set; }
        public string Cover { get; set; }
        public string Classify { get; set; }
        public string Gpntype { get; set; }
        public string Subject { get; set; }
        public string Governance { get; set; }
        public string Grade { get; set; }
        public int Pages { get; set; }
        public string Size { get; set; }
        public string Binding { get; set; }
        public string Language { get; set; }
        public string Introduction { get; set; }
        public string Catalog { get; set; }
        public int Price { get; set; }
        public string TargetPeople { get; set; }
        public string Types { get; set; }
        public string Attachment { get; set; }
        public string Url { get; set; }
        public string Duration { get; set; }
        public string Numbers { get; set; }
        public string Restriction { get; set; }
        public DateTime? CeasedDate { get; set; }
    }
}
