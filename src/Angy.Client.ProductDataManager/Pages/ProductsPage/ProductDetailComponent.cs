using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.ProductsPage
{
    public class ProductDetailComponent : ComponentBase
    {
        [Parameter] public Guid ProductId { get; set; }

        [Inject] public ProductGateway ProductGateway { get; set; } = null!;
        [Inject] public CategoryGateway CategoriesGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected Product Product { get; private set; } = null!;
        protected IEnumerable<Category> Categories { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ProductId == Guid.Empty)
                await InitializeCreateAsync();
            else
                await InitializeUpdateAsync();
        }

        async Task InitializeUpdateAsync()
        {
            var result = await ProductGateway.GetProductByIdWithCategories(ProductId);

            if (result.IsValid)
            {
                var (product, categories) = result.Success;

                Product = product;
                Categories = categories;
                EditContext = new EditContext(Product);
            }

            IsValid = result.IsValid;
        }

        async Task InitializeCreateAsync()
        {
            var result = await CategoriesGateway.GetCategoriesWithIdAndName();

            if (result.IsValid)
            {
                Product = new Product();
                Categories = result.Success;
                EditContext = new EditContext(Product);
            }

            IsValid = result.IsValid;
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (ProductId == Guid.Empty)
                await ProductGateway.CreateProduct(Product);
            else
                await ProductGateway.UpdateProduct(Product, ProductId);

            NavigationManager.NavigateTo("products");
        }
    }
}