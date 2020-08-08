using System;
using System.Linq;
using System.Threading.Tasks;
using Angy.Model;
using Angy.Server.Data;
using Angy.Server.Data.Extensions;
using Angy.Server.Product.GraphQL.Inputs;
using Angy.Server.Product.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Product.GraphQL.RootTypes
{
    public class Mutation : ObjectGraphType<object>
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
                    var product = context.GetArgument<Model.Product>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    return await lucifer.CreateAsync(product);
                });

            FieldAsync<ProductType>(
                "updateProduct",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ProductInputType>> { Name = name }),
                resolve: async context =>
                {
                    var product = context.GetArgument<Model.Product>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    await TrackProductsToBeDelete(lucifer, product);
                    return await lucifer.UpdateAsync(product);
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
                    var category = context.GetArgument<Category>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    return await lucifer.CreateAsync(category);
                });

            FieldAsync<CategoryType>(
                "updateCategory",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<CategoryInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var category = context.GetArgument<Category>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    return await lucifer.UpdateAsync(category);
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
                    var attribute = context.GetArgument<Model.Attribute>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    return await lucifer.CreateAsync(attribute);
                });

            FieldAsync<AttributeType>(
                "updateAttribute",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AttributeInputType>> { Name = name }
                ),
                resolve: async context =>
                {
                    var attribute = context.GetArgument<Model.Attribute>(name);
                    var lucifer = _provider.GetRequiredService<LuciferContext>();

                    return await lucifer.UpdateAsync(attribute);
                });
        }

        protected virtual async Task TrackProductsToBeDelete(LuciferContext lucifer, Model.Product product)
        {
            var descriptions = await lucifer
                .AttributeDescriptions
                .AsNoTracking()
                .Where(description => description.ProductId == product.Id)
                .ToListAsync();

            var toBeDeleted = descriptions.Except(product.Descriptions);
            lucifer.AttributeDescriptions.RemoveRange(toBeDeleted);
        }
    }
}