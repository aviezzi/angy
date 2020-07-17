using System.Collections.Generic;

namespace Angy.Model
{
    public class MicroCategory : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}