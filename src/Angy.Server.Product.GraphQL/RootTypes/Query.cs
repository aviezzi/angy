using System;
using Angy.Model;
using Angy.Server.Data.Abstract;
using Angy.Server.Product.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

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
                async context => await _provider.GetRequiredService<IRepository<Model.Product>>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<ProductType>>("products", "The list of the company products", resolve: async context =>
                await _provider.GetRequiredService<IRepository<Model.Product>>().GetAll());
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
                "A single attribute.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Id" }),
                async context => await _provider.GetRequiredService<IRepository<Model.Attribute>>().GetOne(context.GetArgument<Guid>("id")));

            FieldAsync<ListGraphType<AttributeType>>("attributes", "The list of attributes", resolve: async context =>
                await _provider.GetRequiredService<IRepository<Model.Attribute>>().GetAll());
        }

        void AttributeDescriptionQueries()
        {
            FieldAsync<AttributeDescriptionType>(
                "attributeDescription",
                "A single attribute description of a product.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "Id", Description = "Attribute Description Id" }),
                async context => await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetOne(context.GetArgument<Guid>("id")));
            
            FieldAsync<AttributeDescriptionType>(
                "attributeDescriptionByProductId",
                "The list of attributes description associated to a product.",
                new QueryArguments(new QueryArgument<StringGraphType> { Name = "productId", Description = "product Id" }),
                async context => await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetByProductId(context.GetArgument<Guid>("productId")));
            
            FieldAsync<ListGraphType<AttributeDescriptionType>>("attributeDescriptions", "The list of attribute descriptions", resolve: async context =>
                await _provider.GetRequiredService<IAttributeDescriptionRepository>().GetAll());
        }
    }
}