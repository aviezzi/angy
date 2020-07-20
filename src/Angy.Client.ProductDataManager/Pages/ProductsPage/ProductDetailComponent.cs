using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Client.Shared.ViewModels;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.ProductsPage
{
    public class ProductDetailComponent : ComponentBase
    {
        [Parameter] public Guid ProductId { get; set; }

        [Inject] public ProductGateway ProductGateway { get; set; } = null!;
        [Inject] public MicroCategoryGateway MicroCategoriesGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected ProductViewModel ViewModel { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (ProductId == Guid.Empty)
            {
                var microsResult = await MicroCategoriesGateway.GetMicroCategoriesWithIdAndName();

                if (microsResult.IsValid)
                {
                    ViewModel = new ProductViewModel(microsResult.Success);
                    EditContext = new EditContext(ViewModel);
                }

                IsValid = microsResult.IsValid;

                return;
            }

            var result = await ProductGateway.GetProductByIdWithMicroCategories(ProductId);

            if (result.IsValid)
            {
                var (product, microCategories) = result.Success;

                ViewModel = new ProductViewModel(product, microCategories);
                EditContext = new EditContext(ViewModel);
            }

            IsValid = result.IsValid;
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (ProductId == Guid.Empty)
                await ProductGateway.CreateProduct(ViewModel.Product);
            else
                await ProductGateway.UpdateProduct(ProductId, ViewModel.Product);

            NavigationManager.NavigateTo("products");
        }
    }
}