using System.Collections.Generic;
using Angy.Shared.Model;

namespace Angy.Shared.Responses
{
    public class ProductDetailResponse
    {
        public Product Product { get; set; }
        public IEnumerable<MicroCategory> MicroCategories { get; set; }
    }
}