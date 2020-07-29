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

            ProductMutation();
            MicroCategoryMutation();
            AttributeMutation();
        }

        void ProductMutation()
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

        void MicroCategoryMutation()
        {
            const string name = "microcategory";

            FieldAsync<MicroCategoryType>(
                "createMicroCategory",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<MicroCategoryInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var micro = context.GetArgument<MicroCategory>(name);
                    var created = await lucifer.CreateAsync(micro);

                    return created;
                });

            FieldAsync<MicroCategoryType>(
                "updateMicroCategory",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id" },
                    new QueryArgument<NonNullGraphType<MicroCategoryInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    var id = context.GetArgument<Guid>("id");
                    var micro = context.GetArgument<MicroCategory>(name);

                    var updated = await lucifer.UpdateAsync(id, micro);

                    return updated;
                });
        }

        void AttributeMutation()
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
                    var micro = context.GetArgument<Model.Attribute>(name);

                    var updated = await lucifer.UpdateAsync(id, micro);

                    return updated;
                });
        }
    }
}