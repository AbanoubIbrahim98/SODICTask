using System;
using System.Collections.Generic;

namespace SODICTask.Models
{
    public partial class AttributeValue
    {
        public int AttributeValueId { get; set; }
        public int? ProductId { get; set; }
        public int DynamicAttributeId { get; set; }
        public string Value { get; set; } = null!;

        public virtual DynamicAttribute DynamicAttribute { get; set; } = null!;
        public virtual Product? Product { get; set; } = null!;
    }
}
