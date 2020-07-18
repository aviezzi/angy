using System.Collections.Generic;
using Angy.Model;

namespace Angy.Client.Shared.Responses
{
    public class ProductResponse
    {
        public Product? Product { get; set; }
        public IEnumerable<MicroCategory>? MicroCategories { get; set; }
    }
}