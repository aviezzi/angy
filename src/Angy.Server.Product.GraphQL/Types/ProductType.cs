using System;
using System.Collections.Generic;
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

            Field<MicroCategoryType, MicroCategory>()
                .Name("category")
                .Description("The micro category of the product.")
                .ResolveAsync( async context =>
                {
                    var loader = dataLoader.Context.GetOrAddBatchLoader<Guid, MicroCategory>("GetCategoryByIds", async id =>
                    {
                        var microCategories = await provider.GetRequiredService<IRepository<MicroCategory>>().GetAll();
                        return microCategories.Where(m => id.Contains(m.Id)).ToDictionary(s => s.Id);
                    });

                    return (await loader.LoadAsync(context.Source.MicroCategory.Id));
                });
            
            Field<ListGraphType<AttributeDescriptionType>, IEnumerable<AttributeDescription>>()
                .Name("descriptions")
                .Description("The attributes of the product.")
                .ResolveAsync(async context =>
                {
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, AttributeDescription>("GetAttributesByProductId", async id =>
                    {
                        var descriptions = await provider.GetRequiredService<IAttributeDescriptionRepository>().GetAll();
                        return descriptions.Where(m => id.Contains(m.Product.Id)).ToLookup(s => s.Id);
                    });
            
                    return (await loader.LoadAsync(context.Source.Id));
                });
        }
    }
}