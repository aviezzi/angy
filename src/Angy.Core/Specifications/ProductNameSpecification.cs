using Angy.Core.Model;

namespace Angy.Core.Specifications
{
    public class ProductNameSpecification : SpecificationBase<Product>
    {
        public ProductNameSpecification(string name)
        {
            Criteria = product => product.Name == name;
        }
    }
}