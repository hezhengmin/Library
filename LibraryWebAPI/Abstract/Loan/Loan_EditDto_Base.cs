using LibraryWebAPI.Dtos.LoanDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Abstract.Loan
{
    public abstract class Loan_EditDto_Base : IValidatableObject
    {
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        /// <summary>
        /// 借出開始日期
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// 借出結束日期
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 書籍歸還日期
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ReturnDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dto = (Loan_EditDto_Base)validationContext.ObjectInstance;

            if (DateTime.Compare(dto.IssueDate, dto.DueDate) > 0)
            {
                yield return new ValidationResult("借出開始日期不能大於借出結束日期", new[] { "IssueDate" });
            }

            if (dto.ReturnDate.HasValue)
            {
                //書籍歸還日期 < 借出結束日期 ，回傳-1
                if (DateTime.Compare(dto.ReturnDate.Value, dto.IssueDate) < 0)
                {
                    yield return new ValidationResult("書籍歸還日期不能小於借出結束日期", new[] { "ReturnDate" });
                }
            }
        }
    }
}
