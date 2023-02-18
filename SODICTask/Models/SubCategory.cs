using System;
using System.Collections.Generic;

namespace SODICTask.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            InverseParentSubCategory = new HashSet<SubCategory>();
            Products = new HashSet<Product>();
        }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int? ParentSubCategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Category? Category { get; set; } = null!;
        public virtual SubCategory? ParentSubCategory { get; set; }
        public virtual ICollection<SubCategory> InverseParentSubCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
