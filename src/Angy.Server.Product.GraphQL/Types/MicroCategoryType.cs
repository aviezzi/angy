using Angy.Model;
using GraphQL.Types;

namespace Angy.ProductServer.Core.Types
{
    public sealed class MicroCategoryType : ObjectGraphType<MicroCategory>
    {
        public MicroCategoryType()
        {
            Name = "MicroCategory";
            Description = "A product micro-category";

            Field(d => d.Id).Description("The id of micro-category.");
            Field(d => d.Name).Description("The name of micro-category.");
            Field(d => d.Description).Description("The description of the micro-category.");
        }
    }
}