using System;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.CategoriesPage
{
    public class CategoryDetailComponent : ComponentBase
    {
        [Parameter] public Guid MicroId { get; set; }

        [Inject] public CategoryGateway CategoryGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected Category Category { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (MicroId == Guid.Empty)
            {
                Category = new Category();
                EditContext = new EditContext(Category);
                IsValid = true;

                return;
            }

            var result = await CategoryGateway.GetCategoryById(MicroId);

            if (result.IsValid)
            {
                Category = result.Success;
                EditContext = new EditContext(Category);
            }

            IsValid = result.IsValid;
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (MicroId == Guid.Empty)
                await CategoryGateway.CreateCategory(Category);
            else
                await CategoryGateway.UpdateCategory(MicroId, Category);

            NavigationManager.NavigateTo("/categories");
        }
    }
}