namespace Angy.Model
{
    public class AttributeDescription : EntityBase
    {
        public string Description { get; set; }
        public Product Product { get; set; }
        public Attribute Attribute { get; set; }
    }
}