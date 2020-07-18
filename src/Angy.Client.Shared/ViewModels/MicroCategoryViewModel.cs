using System.ComponentModel.DataAnnotations;
using Angy.Model;

namespace Angy.Client.Shared.ViewModels
{
    public class MicroCategoryViewModel
    {
        public MicroCategory Micro { get; }

        public MicroCategoryViewModel()
            : this(new MicroCategory
            {
                Name = string.Empty,
                Description = string.Empty
            })
        {
        }

        public MicroCategoryViewModel(MicroCategory micro)
        {
            Micro = micro;
        }

        [Required]
        public string Name { get => Micro.Name; set => Micro.Name = value; }

        [Required]
        public string Description { get => Micro.Description; set => Micro.Description = value; }
    }
}