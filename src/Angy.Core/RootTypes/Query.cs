using System;
using Angy.Core.Abstract;
using Angy.Core.Types;
using Angy.Shared.Model;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.Core.RootTypes
{
    public sealed class Query : ObjectGraphType<object>
    {
        readonly IServiceProvider _provider;

        public Query(IServiceProvider provider)
        {
            Name = "Query";

            _provider = provider;

            ProductQueries();
            MicroCategoriesQueries();
        }

        void ProductQueries()
        {
            FieldAsync<ProductType>(
                "product",
                "A single product of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Product Id" }),
                async context => await _provider.GetRequiredService<IRepository<Product>>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<ProductType>>("products", "The list of the company products", resolve: async context =>
                await _provider.GetRequiredService<IRepository<Product>>().GetAll());
        }

        void MicroCategoriesQueries()
        {
            FieldAsync<MicroCategoryType>(
                "microcategory",
                "A single micro category of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Micro Category Id" }),
                async context => await _provider.GetRequiredService<IRepository<MicroCategory>>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<MicroCategoryType>>("microcategories", "The list of the micro categories", resolve: async context =>
                await _provider.GetRequiredService<IRepository<MicroCategory>>().GetAll());
        }
    }
}