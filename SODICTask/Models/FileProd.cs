using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SODICTask.Models
{
    public partial class FileProd
    {
        [Key]
        public int FileId { get; set; }
        public int ProductId { get; set; }
        public string FileName { get; set; } = null!;
        public string FileType { get; set; } = null!;
        public byte[] FileData { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
