using System;
using Angy.Core.Abstract;
using Angy.Core.Inputs;
using Angy.Core.Types;
using Angy.Model.Model;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.Core.RootTypes
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
                    var repository = _provider.GetRequiredService<IRepository<Product>>();

                    var product = context.GetArgument<Product>(name);

                    var created = await repository.Create(product);

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
                    var repository = _provider.GetRequiredService<IRepository<Product>>();

                    var id = context.GetArgument<Guid>("id");
                    var product = context.GetArgument<Product>(name);

                    var updated = await repository.Update(id, product);

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
                    var repository = _provider.GetRequiredService<IRepository<MicroCategory>>();

                    var micro = context.GetArgument<MicroCategory>(name);

                    var created = await repository.Create(micro);

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
                    var repository = _provider.GetRequiredService<IRepository<MicroCategory>>();

                    var id = context.GetArgument<Guid>("id");
                    var micro = context.GetArgument<MicroCategory>(name);

                    var updated = await repository.Update(id, micro);

                    return updated;
                });
        }
    }
}