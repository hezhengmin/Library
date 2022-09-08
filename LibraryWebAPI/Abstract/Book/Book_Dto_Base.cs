using System;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_Dto_Base
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public virtual string Isbn { get; set; }
        public virtual string Issn { get; set; }
        public virtual string Gpn { get; set; }
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
        public virtual string Grade { get; set; }
        public int Pages { get; set; }
        public string Size { get; set; }
        public string Binding { get; set; }
        public string Language { get; set; }
        public string Introduction { get; set; }
        public virtual string Catalog { get; set; }
        public int Price { get; set; }
        public string TargetPeople { get; set; }
        public virtual string Types { get; set; }
        public string Attachment { get; set; }
        /// <summary>
        /// 出版品網址-線上版或試閱版
        /// </summary>
        public virtual string Url { get; set; }
        /// <summary>
        /// 播放時間長度
        /// </summary>
        public virtual string Duration { get; set; }
        /// <summary>
        /// 字號
        /// </summary>
        public virtual string Numbers { get; set; }
        /// <summary>
        /// 權利範圍
        /// </summary>
        public virtual string Restriction { get; set; }
        /// <summary>
        /// 停刊註記
        /// </summary>
        public DateTime? CeasedDate { get; set; }
        public string Authority { get; set; }
    }
}
