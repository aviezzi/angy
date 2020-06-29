using System;
using Angy.Core.Abstract;
using Angy.Core.Extensions;
using Angy.Core.Specifications;
using Angy.Core.Types;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.RootTypes
{
    public class Query : ObjectGraphType<object>
    {
        private readonly IServiceProvider _services;

        public Query(IServiceProvider services)
        {
            Name = "Query";

            _services = services;

            ProductQueries();
            MicroCategoriesQueries();
        }

        private void ProductQueries()
        {
            FieldAsync<ProductType>(
                "product",
                "A single product of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> {Name = "Id", Description = "Product Id"}),
                async context => await _services.GetRequiredService<ILuciferContext>().Products.Specify(new ProductIdSpecification(context.GetArgument<Guid>("id"))).FirstOrDefaultAsync());

            FieldAsync<ListGraphType<ProductType>>("products", "The list of the company products", resolve: async context => await _services.GetRequiredService<ILuciferContext>().Products.ToListAsync());
        }
        
        private void MicroCategoriesQueries()
        {
            FieldAsync<ListGraphType<MicroCategoryType>>("microCategories", "The list of the micro categories", resolve: async context => await _services.GetRequiredService<ILuciferContext>().MicroCategories.ToListAsync());
        }
    }
}