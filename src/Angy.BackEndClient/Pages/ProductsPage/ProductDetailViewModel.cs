using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Angy.Shared.Model;

namespace Angy.BackEndClient.Pages.ProductsPage
{
    using FormValidators;

    public class ProductDetailViewModel
    {
        public ProductDetailViewModel() : this(new List<MicroCategory>())
        {
        }

        public ProductDetailViewModel(IEnumerable<MicroCategory> microCategories) : this(new Product
        {
            Id = default,
            Name = string.Empty,
            Description = string.Empty,
            MicroCategory = new MicroCategory
            {
                Description = string.Empty
            }
        }, microCategories)
        {
        }

        public ProductDetailViewModel(Product product, IEnumerable<MicroCategory> microCategories)
        {
            Product = product;
            MicroCategories = microCategories;
        }

        public Product Product { get; }

        [Required]
        public string Name
        {
            get => Product.Name;
            set => Product.Name = value;
        }

        [Required]
        public string Description
        {
            get => Product.Description;
            set => Product.Description = value;
        }

        [SelectValidator]
        public string MicroCategoryId
        {
            get => Product.MicroCategory.Id.ToString();
            set => Product.MicroCategory.Id = Guid.Parse(value);
        }

        public IEnumerable<MicroCategory> MicroCategories { get; }
    }
}