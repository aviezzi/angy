using System.Collections.Generic;

namespace Angy.Shared.Model
{
    public class MicroCategory : EntityBase
    {
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}