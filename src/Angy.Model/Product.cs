using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class Product : EntityBase
    {
        public Product() : this(string.Empty)
        {
        }

        public Product(string name, MicroCategory? microCategory = default, IEnumerable<AttributeDescription>? descriptions = default) : this(name)
        {
            MicroCategory = microCategory;
            Descriptions = descriptions;
        }

        Product(string name)
        {
            Name = name;
        }

        [Required] public string Name { get; set; }

        public MicroCategory? MicroCategory { get; set; }

        public IEnumerable<AttributeDescription>? Descriptions { get; set; }
    }
}