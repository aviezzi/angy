using System;
using System.Collections.Generic;
using Angy.Shared.Model;

namespace Angy.BackEndClient.Pages.ProductsPage
{
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

        public Product Product { get; set; }
        public IEnumerable<MicroCategory> MicroCategories { get; }

        public string Name
        {
            get => Product.Name;
            set => Product.Name = value;
        }

        public string Description
        {
            get => Product.Description;
            set => Product.Description = value;
        }

        public string MicroCategoryId
        {
            get => Product.MicroCategory.Id.ToString();
            set => Product.MicroCategory.Id = Guid.Parse(value);
        }
    }
}