using System;
using System.Collections.Generic;

namespace SODICTask.Models
{
    public partial class Product
    {
        public Product()
        {
            AttributeValues = new HashSet<AttributeValue>();
            FileProds = new HashSet<FileProd>();
        }

        public int ProductId { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public virtual SubCategory? SubCategory { get; set; } = null!;
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
        public virtual ICollection<FileProd> FileProds { get; set; }
    }
}
