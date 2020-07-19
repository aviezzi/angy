using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;

namespace Angy.Client.ProductDataManager.Pages
{
    public class IndexComponent : ComponentBase
    {
        [Inject] public ProductGateway ProductGateway { get; set; } = null!;

        protected IEnumerable<Product> Products { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await ProductGateway.GetProductsWithIdNameDescriptionAndMicroName();
            IsValid = result.IsValid;

            if (result.IsValid) Products = result.Success;
        }
    }
}