using System.ComponentModel.DataAnnotations;

namespace Angy.Model
{
    public class Product : EntityBase
    {
        [Required] public string Name { get; set; } = null!;
        public MicroCategory MicroCategory { get; set; } = null!;
    }
}