using Angy.Shared.Model;
using GraphQL.Types;

namespace Angy.Core.Inputs
{
    public sealed class MicroCategoryInputType : InputObjectGraphType<MicroCategory>
    {
        public MicroCategoryInputType()
        {
            Name = "MicroCategoryInput";

            Field(x => x.Id);
        }
    }
}