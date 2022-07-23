using System;
using System.Collections.Generic;

namespace Zheng.Infrastructure.Models
{
    public partial class Account
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public byte[] Password { get; set; }
        public DateTime SystemDate { get; set; }
    }
}
