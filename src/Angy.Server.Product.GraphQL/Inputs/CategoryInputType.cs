using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class CategoryInputType : InputObjectGraphType<Category>
    {
        public CategoryInputType()
        {
            Name = "CategoryInput";

            Field(x => x.Id, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Description, nullable: true);
        }
    }
}