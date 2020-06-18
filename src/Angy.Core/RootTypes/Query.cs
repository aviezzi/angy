using System;
using System.Collections.Generic;
using Angy.Core.Model;
using Angy.Core.Types;
using GraphQL.Types;

namespace Angy.Core.RootTypes
{
    public class Query : ObjectGraphType<object>
    {
        private static readonly IEnumerable<Product> Products = new List<Product>
        {
            new Product {Id = Guid.NewGuid(), Name = "Strawberry", Description = "Red Fire", Enabled = true},
            new Product {Id = Guid.NewGuid(), Name = "Watermelon", Description = "Green Leaf", Enabled = true}
        };

        public Query()
        {
            Name = "Query";

            Field<ListGraphType<ProductType>>("products", resolve: context => Products);
        }
    }
}