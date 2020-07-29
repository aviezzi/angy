using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;

namespace Angy.Client.ProductDataManager.Pages.CategoriesPage
{
    public class CategoriesComponent : ComponentBase
    {
        [Inject] public CategoryGateway CategoryGateway { get; set; } = null!;

        protected IEnumerable<Category> Categories { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await CategoryGateway.GetCategoriesWithIdNameAndDescription();

            if (result.IsValid) Categories = result.Success;

            IsValid = result.IsValid;
        }
    }
}