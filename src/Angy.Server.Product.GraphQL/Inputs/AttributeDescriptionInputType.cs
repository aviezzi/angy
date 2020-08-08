using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class AttributeDescriptionInputType : InputObjectGraphType<AttributeDescription>
    {
        public AttributeDescriptionInputType()
        {
            Name = "AttributeDescriptionInput";

            Field(description => description.Id);

            Field(description => description.Description);

            Field(description => description.ProductId);
            Field(description => description.AttributeId);
        }
    }
}