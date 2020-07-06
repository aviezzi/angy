using Angy.Shared.Model;
using GraphQL.Types;

namespace Angy.Core.Inputs
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