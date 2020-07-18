using Angy.Model;
using GraphQL.Types;

namespace Angy.ProductServer.Core.Types
{
    public sealed class AttributeDescriptionType : ObjectGraphType<AttributeDescription>
    {
        public AttributeDescriptionType()
        {
            Name = "Attribute Description";
            Description = "The description of a attribute.";

            Field(d => d.Id).Description("The id of attribute description");
            Field(d => d.Description).Description("The description of the associated attribute.");
        }
    }
}