using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class Category : EntityBase
    {
        public Category() : this(string.Empty, string.Empty)
        {
        }

        public Category(string name, string description, Category? parent = default, IEnumerable<Product>? products = default) : this(name, description)
        {
            ParentCategory = parent;
            Products = products;
        }

        Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        [Required] public string Name { get; set; }

        [Required] public string Description { get; set; }

        public Category? ParentCategory { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}