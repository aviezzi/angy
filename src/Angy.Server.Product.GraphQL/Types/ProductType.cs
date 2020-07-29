using System;
using System.Collections.Generic;
using System.Linq;
using Angy.Model;
using Angy.Server.Data;
using Angy.Server.Data.Extensions;
using Angy.Server.Data.Specifications;
using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQL.Utilities;
using Microsoft.EntityFrameworkCore;

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
            Field(d => d.CategoryId).Description("The id og the product category.");

            Field<MicroCategoryType, MicroCategory>()
                .Name("category")
                .Description("The micro category of the product.")
                .ResolveAsync(async context =>
                {
                    var loader = dataLoader.Context.GetOrAddBatchLoader<Guid, MicroCategory>("GetCategoryByIds", async id =>
                    {
                        var micros = provider.GetRequiredService<LuciferContext>().MicroCategories;

                        return await micros.Specify(new ByIdsSpecification<MicroCategory>(id)).ToDictionaryAsync(e => e.Id);
                    });

                    return await loader.LoadAsync(context.Source.CategoryId);
                });

            Field<ListGraphType<AttributeDescriptionType>, IEnumerable<AttributeDescription>>()
                .Name("descriptions")
                .Description("The attributes of the product.")
                .ResolveAsync(async context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, AttributeDescription>("GetAttributesByProductId", async id =>
                    {
                        var descriptions = provider.GetRequiredService<LuciferContext>().AttributeDescriptions;

                        return (await descriptions.Specify(new ByIdsSpecification<AttributeDescription>(id)).ToListAsync()).ToLookup(e => e.Id);
                    });

                    return await loader.LoadAsync(context.Source.Id);
                });
        }
    }
}