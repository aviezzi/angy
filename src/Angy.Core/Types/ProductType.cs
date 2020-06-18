using Angy.Core.Model;
using GraphQL.Types;

namespace Angy.Core.Types
{
    public sealed class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            Name = "Product";
            Description = "A products sold by the company.";

            Field(d => d.Id).Description("The id of the product.");
            Field(d => d.Name).Description("The name of the product.");
            Field(d => d.Description, true).Description("The description of the product.");
        }
    }
}