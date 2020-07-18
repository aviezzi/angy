﻿using System;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace Angy.ProductServer.Core.Types
{
    public sealed class AttributeType : ObjectGraphType<Model.Attribute>
    {
        public AttributeType()
        {
            Name = "Attribute";
            Description = "A macro attribute associated to the product.";

            Field(d => d.Id).Description("The id of attribute.");
            Field(d => d.Name).Description("The name of attribute.");
        }
    }
}