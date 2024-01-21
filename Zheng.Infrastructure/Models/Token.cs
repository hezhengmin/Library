using System;
using System.Collections.Generic;

#nullable disable

namespace Zheng.Infra.Data.Models
{
    public partial class Token
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevorked { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
