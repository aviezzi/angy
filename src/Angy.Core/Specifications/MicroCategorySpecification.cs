using System;
using Angy.Shared.Model;

namespace Angy.Core.Specifications
{
    public class MicroCategorySpecification : SpecificationBase<MicroCategory>
    {
        public MicroCategorySpecification(Guid id)
        {
            Criteria = product => product.Id == id;
        }
    }
}