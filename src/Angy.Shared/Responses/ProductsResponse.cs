using System.Collections.Generic;
using Angy.Shared.Model;

namespace Angy.Shared.Responses
{
    public class ProductsResponse
    {
        public IEnumerable<Product> Products { get; set; }
    }
}