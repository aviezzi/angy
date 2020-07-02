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
                    var lucifer = _provider.GetRequiredService<ILuciferContext>();

                    var product = context.GetArgument<Product>("product");

                    await lucifer.BeginTransactionAsync();
                    await lucifer.Products.AddAsync(product);
                    await lucifer.CommitAsync();

                    return product;
                });
        }
    }
}