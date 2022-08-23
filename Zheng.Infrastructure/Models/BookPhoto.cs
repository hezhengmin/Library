using System;

#nullable disable

namespace Zheng.Infrastructure.Models
{
    public partial class BookPhoto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid UploadFileId { get; set; }
        public DateTime SystemDate { get; set; }

        public virtual Book Book { get; set; }
    }
}
