using System;
using Angy.Core.Abstract;
using Angy.Core.Inputs;
using Angy.Core.Types;
using Angy.Shared.Model;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.Core.RootTypes
{
    public sealed class Mutation : ObjectGraphType<object>
    {
        private readonly IServiceProvider _provider;

        public Mutation(IServiceProvider provider)
        {
            Name = "Mutation";

            _provider = provider;

            ProductMutation();
        }

        private void ProductMutation()
        {
            FieldAsync<ProductType>(
                "createProduct",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ProductInputType>> {Name = "product"}
                ),
                resolve: async context =>
                {
                    var repository = _provider.GetRequiredService<IRepository<Product>>();

                    var product = context.GetArgument<Product>("product");

                    var created = await repository.Create(product);

                    return created;
                });

            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "id"},
                    new QueryArgument<NonNullGraphType<ProductInputType>> {Name = "product"}
                ),
                resolve: async context =>
                {
                    var repository = _provider.GetRequiredService<IRepository<Product>>();

                    var id = context.GetArgument<Guid>("id");
                    var product = context.GetArgument<Product>("product");

                    var updated = await repository.Update(id, product);

                    return updated;
                });
        }
    }
}