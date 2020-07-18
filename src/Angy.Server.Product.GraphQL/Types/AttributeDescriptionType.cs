using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Types
{
    public sealed class AttributeDescriptionType : ObjectGraphType<AttributeDescription>
    {
        public AttributeDescriptionType()
        {
            Name = "AttributeDescription";
            Description = "The description of an attribute.";

            Field(d => d.Id).Description("The id of attribute description.");
            Field(d => d.Description).Description("The description of the associated attribute.");
        }
    }
}