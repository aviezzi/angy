using System;

namespace Angy.Model
{
    public class AttributeDescription : EntityBase
    {
        public AttributeDescription() : this(string.Empty)
        {
        }

        public AttributeDescription(string description, Guid productId = default, Product product = default, Guid attributeId = default, Attribute attribute = default) : this(description)
        {
            ProductId = productId;
            Product = product;

            AttributeId = attributeId;
            Attribute = attribute;
        }

        AttributeDescription(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid AttributeId { get; set; }
        public Attribute? Attribute { get; set; }
    }
}