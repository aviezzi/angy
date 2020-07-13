using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model;
using Angy.Model.Model;
using Angy.Shared.Gateways;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages
{
    public class IndexComponent : ComponentBase
    {
        [Inject]
        public ProductGateway ProductGateway { get; set; }

        protected IEnumerable<Product> Products { get; private set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var result = await Result.Try(ProductGateway.GetProducts);

            if (result.IsValid)
            {
                Products = result.Success.Products;
            }
        }
    }
}