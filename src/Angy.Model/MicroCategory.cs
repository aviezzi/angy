using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class MicroCategory : EntityBase
    {
        public MicroCategory() : this(string.Empty, string.Empty)
        {
        }

        public MicroCategory(string name, string description, MicroCategory? parent = default, IEnumerable<Product>? products = default) : this(name, description)
        {
            MicroCategoryParent = parent;
            Products = products;
        }

        MicroCategory(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public MicroCategory? MicroCategoryParent { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}