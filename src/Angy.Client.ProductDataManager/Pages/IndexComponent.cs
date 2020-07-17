using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model;
using Angy.Shared.Gateways;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages
{
    public class IndexComponent : ComponentBase
    {
        [Inject] public ProductGateway ProductGateway { get; set; } = null!;

        protected IEnumerable<Product> Products { get; private set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var result = await ProductGateway.GetProductsWithIdNameDescriptionAndMicroName();

            if (result.IsValid) Products = result.Success;
        }
    }
}