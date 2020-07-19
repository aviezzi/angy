using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class AttributeInputType : InputObjectGraphType<Attribute>
    {
        public AttributeInputType()
        {
            Name = "AttributeInput";

            Field(x => x.Name);
        }
    }
}