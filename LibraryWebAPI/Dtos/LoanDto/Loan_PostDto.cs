using System;

namespace LibraryWebAPI.Dtos.LoanDto
{
    public class Loan_PostDto
    {
        public Guid AccountId { get; set; }
        public Guid BookId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
