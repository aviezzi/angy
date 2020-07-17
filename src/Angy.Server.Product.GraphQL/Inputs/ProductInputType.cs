using Angy.Model;
using GraphQL.Types;

namespace Angy.ProductServer.Core.Inputs
{
    public sealed class ProductInputType : InputObjectGraphType<Product>
    {
        public ProductInputType()
        {
            Name = "ProductInput";

            Field(x => x.Name);
            Field(x => x.Description);
            Field<MicroCategoryInputType>("microcategory");
        }
    }
}