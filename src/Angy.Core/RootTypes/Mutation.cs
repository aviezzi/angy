using System;
using Angy.Core.Abstract;
using Angy.Core.Extensions;
using Angy.Core.Inputs;
using Angy.Core.Specifications;
using Angy.Core.Types;
using Angy.Shared.Model;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.EntityFrameworkCore;

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

            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "id"},
                    new QueryArgument<NonNullGraphType<ProductInputType>> {Name = "product"}
                ),
                resolve: async context =>
                {
                    var lucifer = _provider.GetRequiredService<ILuciferContext>();

                    var id = context.GetArgument<Guid>("id");
                    var product = context.GetArgument<Product>("product");

                    var entity = await lucifer.Products.Specify(new ProductIdSpecification(id)).FirstOrDefaultAsync();
                    entity.Name = product.Name;
                    entity.Description = product.Description;

                    await lucifer.CommitAsync();

                    return entity;
                });
        }
    }
}