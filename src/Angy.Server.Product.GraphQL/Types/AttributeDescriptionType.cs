using System;
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
    public sealed class AttributeDescriptionType : ObjectGraphType<AttributeDescription>
    {
        public AttributeDescriptionType(IServiceProvider provider, IDataLoaderContextAccessor dataLoader)
        {
            Name = "AttributeDescription";
            Description = "The description of an attribute.";

            Field(d => d.Id).Description("The id of attribute description.");
            Field(d => d.Description).Description("The description of the associated attribute.");
            Field(d => d.AttributeId).Description("The id of associate attribute");
            Field(d => d.ProductId).Description("The id of associate product");

            Field<AttributeType, Model.Attribute>()
                .Name("attribute")
                .Description("The atrribute of the description.")
                .ResolveAsync(async context =>
                {
                    var loader = dataLoader.Context.GetOrAddBatchLoader<Guid, Model.Attribute>("GetAttributesByIds", async id =>
                    {
                        var lucifer = provider.GetRequiredService<LuciferContext>();

                        return await lucifer.Attributes.Specify(new ByIdsSpecification<Model.Attribute>(id)).ToDictionaryAsync(e => e.Id);
                    });

                    return await loader.LoadAsync(context.Source.AttributeId);
                });
        }
    }
}