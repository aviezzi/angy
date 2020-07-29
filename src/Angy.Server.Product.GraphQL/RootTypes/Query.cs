using System;
using Angy.Model;
using Angy.Server.Data;
using Angy.Server.Data.Extensions;
using Angy.Server.Data.Specifications;
using Angy.Server.Product.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Product.GraphQL.RootTypes
{
    public sealed class Query : ObjectGraphType<object>
    {
        readonly IServiceProvider _provider;

        public Query(IServiceProvider provider)
        {
            Name = "Query";

            _provider = provider;

            ProductQueries();
            CategoryQueries();
            AttributeQueries();
            AttributeDescriptionQueries();
        }

        void ProductQueries()
        {
            FieldAsync<ProductType>(
                "product",
                "A single product of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Product Id" }),
                async context =>
                {
                    var products = _provider.GetRequiredService<LuciferContext>().Products;
                    var id = context.GetArgument<Guid>("id");

                    return await products.Specify(new ByIdSpecification<Model.Product>(id)).FirstOrDefaultAsync();
                });

            FieldAsync<ListGraphType<ProductType>>(
                "products",
                "The list of the company products",
                resolve: async context =>
                {
                    var products = _provider.GetRequiredService<LuciferContext>().Products;

                    return await products.ToListAsync();
                });
        }

        void CategoryQueries()
        {
            FieldAsync<CategoryType>(
                "category",
                "A single category of the company.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Category Id" }),
                async context =>
                {
                    var micros = _provider.GetRequiredService<LuciferContext>().Categories;
                    var id = context.GetArgument<Guid>("id");

                    return await micros.Specify(new ByIdSpecification<Category>(id)).FirstOrDefaultAsync();
                });

            FieldAsync<ListGraphType<CategoryType>>(
                "categories",
                "The list of the categories",
                resolve: async context =>
                {
                    var micros = _provider.GetRequiredService<LuciferContext>().Categories;

                    return await micros.ToListAsync();
                });
        }

        void AttributeQueries()
        {
            FieldAsync<AttributeType>(
                "attribute",
                "A single attribute.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Id" }),
                async context =>
                {
                    var attributes = _provider.GetRequiredService<LuciferContext>().Attributes;
                    var id = context.GetArgument<Guid>("id");

                    return await attributes.Specify(new ByIdSpecification<Model.Attribute>(id)).FirstOrDefaultAsync();
                });

            FieldAsync<ListGraphType<AttributeType>>(
                "attributes",
                "The list of attributes", resolve: async context =>
                {
                    var attributes = _provider.GetRequiredService<LuciferContext>().Attributes;

                    return await attributes.ToListAsync();
                });
        }

        void AttributeDescriptionQueries()
        {
            FieldAsync<AttributeDescriptionType>(
                "description",
                "A single attribute description of a product.",
                new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Description Id" },
                    new QueryArgument<StringGraphType> { Name = "productId", Description = "product Id" }),
                async context =>
                {
                    var descriptions = _provider.GetRequiredService<LuciferContext>().AttributeDescriptions;
                    var id = context.GetArgument<Guid>("id");

                    return await descriptions.Specify(new ByIdSpecification<AttributeDescription>(id)).FirstOrDefaultAsync();
                });

            FieldAsync<ListGraphType<AttributeDescriptionType>>(
                "descriptions",
                "The list of attribute descriptions",
                resolve: async context =>
                {
                    var descriptions = _provider.GetRequiredService<LuciferContext>().AttributeDescriptions;

                    return await descriptions.ToListAsync();
                });
        }
    }
}