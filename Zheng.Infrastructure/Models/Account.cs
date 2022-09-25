using System;
using System.Collections.Generic;

#nullable disable

namespace Zheng.Infrastructure.Models
{
    public partial class Account
    {
        public Account()
        {
            Loans = new HashSet<Loan>();
        }

        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public byte[] Password { get; set; }
        public string Email { get; set; }
        public DateTime SystemDate { get; set; }

        public virtual ICollection<Loan> Loans { get; set; }
    }
}
