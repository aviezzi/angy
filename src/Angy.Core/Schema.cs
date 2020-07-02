using System;
using Angy.Core.RootTypes;
using GraphQL.Utilities;

namespace Angy.Core
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