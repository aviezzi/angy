namespace Angy.Model
{
    public class AttributeDescription : EntityBase
    {
        public AttributeDescription() : this(string.Empty)
        {
        }

        public AttributeDescription(string description, Product? product = default, Attribute? attribute = default) : this(description)
        {
            Product = product;
            Attribute = attribute;
        }

        AttributeDescription(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
        public Product? Product { get; set; }
        public Attribute? Attribute { get; set; }
    }
}