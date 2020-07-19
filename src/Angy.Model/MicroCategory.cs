using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class MicroCategory : EntityBase
    {
        [Required] public string? Name { get; set; }

        [Required] public string? Description { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}