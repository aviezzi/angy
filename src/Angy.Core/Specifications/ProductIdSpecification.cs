using System;
using Angy.Model.Model;

namespace Angy.Core.Specifications
{
    public class ProductIdSpecification : SpecificationBase<Product>
    {
        public ProductIdSpecification(Guid id)
        {
            Criteria = product => product.Id == id;
            Includes.Add(product => product.MicroCategory);
        }
    }
}