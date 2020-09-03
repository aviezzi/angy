using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Angy.Model.Abstract;

namespace Angy.Model
{
    public class Attribute : EntityBase
    {
        [Required] public string? Name { get; set; }

        public IEnumerable<AttributeDescription>? Descriptions { get; set; }
    }
}