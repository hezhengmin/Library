using System;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_Dto_Base
    {
        public Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual int Status { get; set; }
        public virtual string Isbn { get; set; }
        public virtual string Issn { get; set; }
        public virtual string Gpn { get; set; }
        public virtual string Publisher { get; set; }
        public virtual string RightCondition { get; set; }
        public virtual string Creator { get; set; }
        public virtual string PublishDate { get; set; }
        public virtual string Edition { get; set; }
        public virtual string Cover { get; set; }
        public virtual string Classify { get; set; }
        public virtual string Gpntype { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Governance { get; set; }
        public virtual string Grade { get; set; }
        public virtual int Pages { get; set; }
        public virtual string Size { get; set; }
        public virtual string Binding { get; set; }
        public virtual string Language { get; set; }
        public virtual string Introduction { get; set; }
        public virtual string Catalog { get; set; }
        public virtual int Price { get; set; }
        public virtual string TargetPeople { get; set; }
        public virtual string Types { get; set; }
        public virtual string Attachment { get; set; }
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
        public virtual string Authority { get; set; }
    }
}
