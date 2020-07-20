using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class ProductInputType : InputObjectGraphType<Model.Product>
    {
        public ProductInputType()
        {
            Name = "ProductInput";

            Field(x => x.Name);
            Field<MicroCategoryInputType>("microcategory");
        }
    }
}