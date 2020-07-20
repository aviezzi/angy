using System;
using System.Linq;
using Angy.Model;
using Angy.Server.Data.Abstract;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Utilities;

namespace Angy.Server.Product.GraphQL.Types
{
    public sealed class ProductType : ObjectGraphType<Model.Product>
    {
        public ProductType(IServiceProvider provider, IDataLoaderContextAccessor dataLoader)
        {
            Name = "Product";
            Description = "A products sold by the company.";

            Field(d => d.Id).Description("The id of the product.");
            Field(d => d.Name).Description("The name of the product.");

            FieldAsync<MicroCategoryType>("microcategory", "The micro category of the product.",
                new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "id", Description = "id of the product" }
                ),
                async context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, MicroCategory>("GetProductByMicroCategoryIds", async id =>
                    {
                        var microCategories = await provider.GetRequiredService<IRepository<MicroCategory>>().GetAll();
                        return microCategories.Where(m => id.Contains(m.Id)).ToLookup(s => s.Id);
                    });

                    return (await loader.LoadAsync(context.Source.MicroCategory.Id)).FirstOrDefault();
                });
        }
    }
}