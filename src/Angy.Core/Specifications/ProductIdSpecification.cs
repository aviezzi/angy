using System;
using Angy.Shared.Model;

namespace Angy.Core.Specifications
{
    public class ProductIdSpecification : SpecificationBase<Product>
    {
        public ProductIdSpecification(Guid id)
        {
            Criteria = product => product.Id == id;
        }
    }
}