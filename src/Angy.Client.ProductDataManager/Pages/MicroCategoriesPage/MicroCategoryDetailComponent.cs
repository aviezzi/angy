using System;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.MicroCategoriesPage
{
    public class MicroCategoryDetailComponent : ComponentBase
    {
        [Parameter] public Guid MicroId { get; set; }

        [Inject] public MicroCategoryGateway MicroCategoryGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected MicroCategory Micro { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (MicroId == Guid.Empty)
            {
                Micro = new MicroCategory();
                EditContext = new EditContext(Micro);
                IsValid = true;

                return;
            }

            var result = await MicroCategoryGateway.GetMicroCategoryById(MicroId);
            
            if (result.IsValid)
            {
                Micro = result.Success;
                EditContext = new EditContext(Micro);
            }
            
            IsValid = result.IsValid;
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (MicroId == Guid.Empty)
                await MicroCategoryGateway.CreateMicroCategory(Micro);
            else
                await MicroCategoryGateway.UpdateMicroCategory(MicroId, Micro);

            NavigationManager.NavigateTo("/micro-categories");
        }
    }
}