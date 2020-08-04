using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class ProductInputType : InputObjectGraphType<Model.Product>
    {
        public ProductInputType()
        {
            Name = "ProductInput";

            Field(product => product.Id, nullable: true);
            Field(product => product.Name);
            Field(product => product.CategoryId);
            Field<ListGraphType<AttributeDescriptionInputType>>("descriptions");
        }
    }
}