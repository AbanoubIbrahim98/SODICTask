using System;
using System.Collections.Generic;

namespace SODICTask.Models
{
    public partial class Category
    {
        public Category()
        {
            DynamicAttributes = new HashSet<DynamicAttribute>();
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DynamicAttribute> DynamicAttributes { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
