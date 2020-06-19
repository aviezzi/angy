using Angy.Core.Abstract;
using Angy.Core.Extensions;
using Angy.Core.Specifications;
using Angy.Core.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.RootTypes
{
    public class Query : ObjectGraphType<object>
    {
        private readonly ILuciferContext _lucifer;

        public Query(ILuciferContext lucifer)
        {
            Name = "Query";

            _lucifer = lucifer;

            ProductQueries();
        }

        private void ProductQueries()
        {
            FieldAsync<ProductType>(
                "product",
                "A single product of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> {Name = "Name", Description = "name of the product"}),
                async context => await _lucifer.Products.Specify(new ProductNameSpecification(context.GetArgument<string>("name"))).FirstOrDefaultAsync());

            FieldAsync<ListGraphType<ProductType>>("products", "The list of the company products", resolve: async context => await _lucifer.Products.ToListAsync());
        }
    }
}