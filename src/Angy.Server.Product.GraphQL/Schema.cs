using System;
using Angy.ProductServer.Core.RootTypes;
using GraphQL.Utilities;

namespace Angy.ProductServer.Core
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider services) : base(services)
        {
            Query = services.GetRequiredService<Query>();
            Mutation = services.GetRequiredService<Mutation>();
        }
    }
}