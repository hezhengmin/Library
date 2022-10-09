using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Book
{
    public abstract class Book_Dto_Base
    {
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Display(Name = "書名標題")]
        public virtual string Title { get; set; }

        [Display(Name = "狀態(0有庫存1無庫存)")]
        public virtual int Status { get; set; }

        [Display(Name = "庫存數量")]
        public virtual int NumberOfCopies { get; set; }

        [Display(Name = "ISBN")]
        public virtual string Isbn { get; set; }

        [Display(Name = "ISSN")]
        public virtual string Issn { get; set; }

        [Display(Name = "GPN(政府出版品統一編號)")]
        public virtual string Gpn { get; set; }

        [Display(Name = "出版單位")]
        public virtual string Publisher { get; set; }

        [Display(Name = "出版情況")]
        public virtual string RightCondition { get; set; }

        [Display(Name = "作者資訊")]
        public virtual string Creator { get; set; }

        [Display(Name = "出版日期")]
        public virtual DateTime PublishDate { get; set; }

        [Display(Name = "版次")]
        public virtual string Edition { get; set; }

        [Display(Name = "書封連結")]
        public virtual string Cover { get; set; }

        [Display(Name = "書籍分類")]
        public virtual string Classify { get; set; }

        [Display(Name = "出版品分類")]
        public virtual string Gpntype { get; set; }

        [Display(Name = "主題分類")]
        public virtual string Subject { get; set; }

        [Display(Name = "施政分類")]
        public virtual string Governance { get; set; }

        [Display(Name = "級別")]
        public virtual string Grade { get; set; }

        [Display(Name = "頁數")]
        public virtual int Pages { get; set; }

        [Display(Name = "開數")]
        public virtual string Size { get; set; }

        [Display(Name = "裝訂")]
        public virtual string Binding { get; set; }

        [Display(Name = "語言")]
        public virtual string Language { get; set; }

        [Display(Name = "書籍介紹")]
        public virtual string Introduction { get; set; }

        [Display(Name = "目次")]
        public virtual string Catalog { get; set; }

        [Display(Name = "價格")]
        public virtual int Price { get; set; }

        [Display(Name = "適用對象")]
        public virtual string TargetPeople { get; set; }

        [Display(Name = "資料類型")]
        public virtual string Types { get; set; }

        [Display(Name = "附件")]
        public virtual string Attachment { get; set; }

        /// <summary>
        /// 出版品網址-線上版或試閱版
        /// </summary>
        [Display(Name = "出版品網址-線上版或試閱版")]
        public virtual string Url { get; set; }

        /// <summary>
        /// 播放時間長度
        /// </summary>
        [Display(Name = "播放時間長度")]
        public virtual string Duration { get; set; }

        /// <summary>
        /// 字號
        /// </summary>
        [Display(Name = "字號")]
        public virtual string Numbers { get; set; }

        /// <summary>
        /// 權利範圍
        /// </summary>
        [Display(Name = "權利範圍")]
        public virtual string Restriction { get; set; }

        /// <summary>
        /// 停刊註記
        /// </summary>
        [Display(Name = "停刊註記")]
        public DateTime? CeasedDate { get; set; }

        [Display(Name = "授權資訊")]
        public virtual string Authority { get; set; }
    }
}
