using System.Collections.Generic;

namespace Angy.Model
{
    public class Attribute : EntityBase
    {
        public string Name { get; set; }
        public IEnumerable<AttributeDescription> Descriptions { get; set; }
    }
}