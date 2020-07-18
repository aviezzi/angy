using System;
using Angy.Server.Product.GraphQL.RootTypes;
using GraphQL.Utilities;

namespace Angy.Server.Product.GraphQL
{
    public class Schema : global::GraphQL.Types.Schema
    {
        public Schema(IServiceProvider services) : base(services)
        {
            Query = services.GetRequiredService<Query>();
            Mutation = services.GetRequiredService<Mutation>();
        }
    }
}