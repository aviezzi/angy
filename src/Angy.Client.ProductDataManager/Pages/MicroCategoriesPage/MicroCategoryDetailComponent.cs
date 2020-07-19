using System;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Client.Shared.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Angy.Client.ProductDataManager.Pages.MicroCategoriesPage
{
    public class MicroCategoryDetailComponent : ComponentBase
    {
        [Parameter] public Guid MicroId { get; set; }

        [Inject] public MicroCategoryGateway MicroCategoryGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected MicroCategoryViewModel ViewModel { get; private set; } = new MicroCategoryViewModel();

        protected override async Task OnInitializedAsync()
        {
            if (MicroId != Guid.Empty)
            {
                var response = await MicroCategoryGateway.GetMicroCategoryById(MicroId);

                if (response.IsValid == true) ViewModel = new MicroCategoryViewModel(response.Success);
            }
        }

        protected async Task HandleValidSubmit()
        {
            if (ViewModel.Micro.Id == Guid.Empty)
                await MicroCategoryGateway.CreateMicroCategory(ViewModel.Micro);
            else
                await MicroCategoryGateway.UpdateMicroCategory(MicroId, ViewModel.Micro);

            NavigationManager.NavigateTo("/micro-categories");
        }
    }
}