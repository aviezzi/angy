using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class Product : EntityBase
    {
        public Product() : this(string.Empty)
        {
        }

        public Product(string name, Category category = default, ICollection<AttributeDescription> descriptions = default) : this(name)
        {
            Category = category;
            Descriptions = descriptions;
        }

        Product(string name)
        {
            Name = name;
        }

        [Required] public string Name { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        [ValidateComplexType, Required] public ICollection<AttributeDescription> Descriptions { get; set; } = new List<AttributeDescription>();
    }
}