using Angy.Shared.Model;
using GraphQL.Types;

namespace Angy.Core.Types
{
    public sealed class MicroCategoryType : ObjectGraphType<MicroCategory>
    {
        public MicroCategoryType()
        {
            Name = "MicroCategory";
            Description = "A product micro-category";

            Field(d => d.Id).Description("The id of micro-category");
            Field(d => d.Description).Description("The description of the micro-category.");
        }
    }
}