using System;
using System.Collections.Generic;
using Angy.Shared.Model;

namespace Angy.BackEndClient.Pages.ProductsPage
{
    public class ProductDetailViewModel
    {
        public ProductDetailViewModel() : this(new Product
        {
            Name = string.Empty,
            Description = string.Empty, 
            MicroCategory = new MicroCategory
            {
                Description = string.Empty
            }
        }, new List<MicroCategory>())
        {
        }

        public ProductDetailViewModel(Product product, IEnumerable<MicroCategory> microCategories)
        {
            Product = product;
            MicroCategories = microCategories;
        }

        public Product Product { get; }
        public IEnumerable<MicroCategory> MicroCategories { get; }

        public string Name
        {
            get => Product.Name;
            set => Product.Description = value;
        }

        public string Description
        {
            get => Product.Name;
            set => Product.Description = value;
        }

        public Guid MicroId
        {
            get => Product.MicroCategory.Id;
            set => Product.MicroCategory.Id = value;
        }
    }
}