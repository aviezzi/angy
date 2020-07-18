using System;
using Angy.Model;

namespace Angy.Server.Data.Specifications
{
    class MicroCategoryGetByIdSpecification : SpecificationBase<MicroCategory>
    {
        public MicroCategoryGetByIdSpecification(Guid id)
        {
            Criteria = product => product.Id == id;
        }
    }
}