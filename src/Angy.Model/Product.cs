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

        public Product(string name, MicroCategory? category = default, IEnumerable<AttributeDescription>? descriptions = default) : this(name)
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
        public MicroCategory? Category { get; set; }

        public IEnumerable<AttributeDescription>? Descriptions { get; set; }
    }
}