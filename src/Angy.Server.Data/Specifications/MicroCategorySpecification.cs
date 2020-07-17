using System;
using Angy.Model;

namespace Angy.Server.Data.Specifications
{
    class MicroCategorySpecification : SpecificationBase<MicroCategory>
    {
        public MicroCategorySpecification(Guid id)
        {
            Criteria = product => product.Id == id;
        }
    }
}