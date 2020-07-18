using System;

namespace Angy.Server.Data.Specifications
{
    class AttributeIdSpecification : SpecificationBase<Model.Attribute>
    {
        public AttributeIdSpecification(Guid id)
        {
            Criteria = attribute => attribute.Id == id;
            Includes.Add(attribute => attribute.Descriptions);
        }
    }
}