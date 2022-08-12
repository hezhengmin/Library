using System;
using System.Collections.Generic;

#nullable disable

namespace Zheng.Infrastructure.Models
{
    public partial class File
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public string Path { get; set; }
        public int Length { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
