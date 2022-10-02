using LibraryWebAPI.Abstract.Loan;
using System;

namespace LibraryWebAPI.Dtos.LoanDto
{
    public class Loan_PutDto : Loan_EditDto_Base
    {
        public Guid Id { get; set; }
    }
}
