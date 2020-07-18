using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Types
{
    public sealed class AttributeType : ObjectGraphType<Attribute>
    {
        public AttributeType()
        {
            Name = "Attribute";
            Description = "A macro attribute associated to the product.";

            Field(d => d.Id).Description("The id of attribute.");
            Field(d => d.Name).Description("The name of attribute.");
        }
    }
}