using System;
using Angy.Model;
using Angy.Server.Data;
using Angy.Server.Data.Extensions;
using Angy.Server.Product.GraphQL.Inputs;
using Angy.Server.Product.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.Server.Product.GraphQL.RootTypes
{
    public sealed class Mutation : ObjectGraphType<object>
    {
        readonly IServiceProvider _provider;

        public Mutation(IServiceProvider provider)
        {
            Name = "Mutation";

            _provider = provider;

            ProductMutations();
            CategoryMutations();
            AttributeMutations();
        }

        void ProductMutations()
        {
            const string name = "product";

            FieldAsync<ProductType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var product = context.GetArgument<Model.Product>(name);
                    var created = await lucifer.CreateAsync(product);

                    return created;
                });

            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" },
                    new QueryArgument<NonNullGraphType<ProductInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var product = context.GetArgument<Model.Product>(name);

                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var updated = await lucifer.UpdateAsync(id, product);

                    return updated;
                });
        }

        void CategoryMutations()
        {
            const string name = "category";

            FieldAsync<CategoryType>(
                "createCategory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var category = context.GetArgument<Category>(name);
                    var created = await lucifer.CreateAsync(category);

                    return created;
                });

            FieldAsync<CategoryType>(
                "updateCategory",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" },
                    new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var id = context.GetArgument<Guid>("id");
                    var category = context.GetArgument<Category>(name);

                    var updated = await lucifer.UpdateAsync(id, category);

                    return updated;
                });
        }

        void AttributeMutations()
        {
            const string name = "attribute";

            FieldAsync<AttributeType>(
                "createAttribute",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<AttributeInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var attr = context.GetArgument<Model.Attribute>(name);
                    var created = await lucifer.CreateAsync(attr);

                    return created;
                });

            FieldAsync<AttributeType>(
                "updateAttribute",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" },
                    new QueryArgument<NonNullGraphType<AttributeInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var id = context.GetArgument<Guid>("id");
                    var attribute = context.GetArgument<Model.Attribute>(name);

                    var updated = await lucifer.UpdateAsync(id, attribute);

                    return updated;
                });
        }
    }
}