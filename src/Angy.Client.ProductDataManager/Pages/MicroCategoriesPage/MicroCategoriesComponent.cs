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

        protected IEnumerable<MicroCategory> MicroCategories { get; private set; } = new List<MicroCategory>();

        protected override async Task OnInitializedAsync()
        {
            var response = await MicroCategoryGateway.GetMicroCategoriesWithIdNameAndDescription();

            if (response.IsValid == true) MicroCategories = response.Success;
        }
    }
}