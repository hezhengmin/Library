using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Dtos.LoanDto
{
    public class Loan_GetDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid BookId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime IssueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DueDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? ReturnDate { get; set; }


        public string BookTitle { get; set; }
        public string UserId { get; set; }

    }
}
