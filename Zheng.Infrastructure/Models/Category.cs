using System;
using System.Collections.Generic;

#nullable disable

namespace Zheng.Infra.Data.Models
{
    public partial class Category
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
