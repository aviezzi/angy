using System;
using System.Collections.Generic;

namespace Angy.Shared.Model
{
    public class MicroCategory
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}