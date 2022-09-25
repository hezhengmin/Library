using System;

namespace LibraryWebAPI.Dtos.LoanDto
{
    public class Loan_GetDto
    {
        public Guid Id { get; set; }
        public Guid Accountd { get; set; }
        public Guid BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public string BookTitle { get; set; }
        public string UserId { get; set; }

    }
}
