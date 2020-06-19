using System;
using System.Linq;
using Angy.Core.Abstract;
using Angy.Core.Model;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.Types
{
    public sealed class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ILuciferContext lucifer, IDataLoaderContextAccessor dataLoader)
        {
            Name = "Product";
            Description = "A products sold by the company.";

            Field(d => d.Id).Description("The id of the product.");
            Field(d => d.Name).Description("The name of the product.");
            Field(d => d.Description, true).Description("The description of the product.");

            FieldAsync<MicroCategoryType>("microcategory", "The micro category of the product.",
                new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "id", Description = "id of the product"}
                ),
                async context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, MicroCategory>("GetProductByMicroCategoryIds", async id =>
                        (await lucifer.MicroCategories.Where(m => id.Contains(m.Id)).ToListAsync()).ToLookup(s => s.Id));

                    return (await loader.LoadAsync(context.Source.Id)).FirstOrDefault();
                });
        }
    }
}