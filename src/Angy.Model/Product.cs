using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    using Annotations;

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

        [ValidateComplexType, NonEmpty(ErrorMessage = "Aggiungi almeno un attributo.")] public ICollection<AttributeDescription> Descriptions { get; set; } = new List<AttributeDescription>();
    }
}