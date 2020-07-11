using System;
using Angy.Model.Model;

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