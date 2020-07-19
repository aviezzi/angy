using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Angy.Model
{
    public class Attribute : EntityBase
    {
        [Required]
        public string Name { get; set; }
        public IEnumerable<AttributeDescription> Descriptions { get; set; }
    }
}