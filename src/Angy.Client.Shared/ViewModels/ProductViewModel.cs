using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Angy.Client.Shared.FormValidators;
using Angy.Model;

namespace Angy.Client.Shared.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
            : this(new List<MicroCategory>())
        {
        }

        public ProductViewModel(IEnumerable<MicroCategory> microCategories)
            : this(new Product { MicroCategory = new MicroCategory() }, microCategories)
        {
        }

        public ProductViewModel(Product product, IEnumerable<MicroCategory> microCategories)
        {
            Product = product;
            MicroCategories = microCategories;
        }

        public Product Product { get; }

        [Required]
        public string Name { get => Product.Name; set => Product.Name = value; }

        [Required]
        public string Description { get => Product.Description; set => Product.Description = value; }

        [SelectValidator]
        public string MicroCategoryId { get => Product.MicroCategory.Id.ToString(); set => Product.MicroCategory.Id = Guid.Parse(value); }

        public IEnumerable<MicroCategory> MicroCategories { get; }
    }
}