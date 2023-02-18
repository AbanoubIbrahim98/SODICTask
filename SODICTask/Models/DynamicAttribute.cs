using System;
using System.Collections.Generic;

namespace SODICTask.Models
{
    public partial class DynamicAttribute
    {
        public DynamicAttribute()
        {
            AttributeValues = new HashSet<AttributeValue>();
        }

        public int DynamicAttributeId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string AttributeType { get; set; } = null!;
        public bool IsMultipleSelect { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
