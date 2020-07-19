using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;

namespace Angy.Client.ProductDataManager.Pages.MicroCategoriesPage
{
    public class MicroCategoriesComponent : ComponentBase
    {
        [Inject] public MicroCategoryGateway MicroCategoryGateway { get; set; } = null!;

        protected IEnumerable<MicroCategory> MicroCategories { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await MicroCategoryGateway.GetMicroCategoriesWithIdNameAndDescription();

            IsValid = result.IsValid;
            if (result.IsValid) MicroCategories = result.Success;
        }
    }
}