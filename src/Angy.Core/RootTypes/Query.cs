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
    public sealed class Query : ObjectGraphType<object>
    {
        private readonly IServiceProvider _provider;

        public Query(IServiceProvider provider)
        {
            Name = "Query";

            _provider = provider;

            ProductQueries();
            MicroCategoriesQueries();
        }

        private void ProductQueries()
        {
            FieldAsync<ProductType>(
                "product",
                "A single product of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> {Name = "Id", Description = "Product Id"}),
                async context => await _provider.GetRequiredService<ILuciferContext>().Products.Specify(new ProductIdSpecification(context.GetArgument<Guid>("id"))).FirstOrDefaultAsync());

            FieldAsync<ListGraphType<ProductType>>("products", "The list of the company products", resolve: async context => await _provider.GetRequiredService<ILuciferContext>().Products.ToListAsync());
        }

        private void MicroCategoriesQueries()
        {
            FieldAsync<ListGraphType<MicroCategoryType>>("microCategories", "The list of the micro categories", resolve: async context => await _provider.GetRequiredService<ILuciferContext>().MicroCategories.ToListAsync());
        }
    }
}