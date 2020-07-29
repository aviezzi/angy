using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class ProductInputType : InputObjectGraphType<Model.Product>
    {
        public ProductInputType()
        {
            Name = "ProductInput";

            Field(product => product.Id);
            Field(product => product.Name);
            Field(product => product.CategoryId);
        }
    }
}