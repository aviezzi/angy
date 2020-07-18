using System;
using Angy.Model;
using Angy.ProductServer.Core.Types;
using Angy.Server.Data.Abstract;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.ProductServer.Core.RootTypes
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
            AttributeQueries();
            AttributeDescriptionQueries();
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

        void AttributeQueries()
        {
            FieldAsync<AttributeType>(
                "attribute",
                "A single attribute of a product.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Id" }),
                async context => await _provider.GetRequiredService<IRepository<Model.Attribute>>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<MicroCategoryType>>("attributes", "The list of attributes", resolve: async context =>
                await _provider.GetRequiredService<IRepository<Model.Attribute>>().GetAll());
        }

        void AttributeDescriptionQueries()
        {
            FieldAsync<AttributeDescriptionType>(
                "attribute description",
                "A single description of a product.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Id" }),
                async context => await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<AttributeDescriptionType>(
                "attribute description by product id",
                "The list of attributes description associated to a product.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "productId", Description = "product Id" }),
                async context => await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetByProductId(context.GetArgument<Guid>("productId")));

            FieldAsync<ListGraphType<MicroCategoryType>>("attributes", "The list of attributes", resolve: async context =>
                await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetAll());
        }
    }
}