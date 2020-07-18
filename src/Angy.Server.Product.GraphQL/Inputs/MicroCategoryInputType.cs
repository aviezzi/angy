﻿using Angy.Model;
using GraphQL.Types;

namespace Angy.Server.Product.GraphQL.Inputs
{
    public sealed class MicroCategoryInputType : InputObjectGraphType<MicroCategory>
    {
        public MicroCategoryInputType()
        {
            Name = "MicroCategoryInput";

            Field(x => x.Id, nullable: true);
            Field(x => x.Name, nullable: true);
            Field(x => x.Description, nullable: true);
        }
    }
}