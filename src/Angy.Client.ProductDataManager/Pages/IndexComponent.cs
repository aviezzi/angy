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

        protected IEnumerable<Product> Products { get; private set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var result = await ProductGateway.GetProductsWithIdNameDescriptionAndMicroName();

            if (result.IsValid == true) Products = result.Success;
        }
    }
}