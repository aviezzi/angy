using System;
using Angy.Model;

namespace Angy.Server.Data.Specifications
{
    class AttributeDescriptionGetByIdSpecification : SpecificationBase<AttributeDescription>
    {
        public AttributeDescriptionGetByIdSpecification(Guid id)
        {
            Criteria = description => description.Id == id;
        }
    }
    
    class AttributeDescriptionGetByProductIdSpecification : SpecificationBase<AttributeDescription>
    {
        public AttributeDescriptionGetByProductIdSpecification(Guid id)
        {
            Criteria = description => description.Product.Id == id;
            Includes.Add(description => description.Attribute);
        }
    }
}