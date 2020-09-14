using System;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.AttributesPage
{
    public class AttributeDetailComponent : ComponentBase
    {
        [Parameter] public Guid AttributeId { get; set; }

        [Inject] public AttributeGateway AttributeGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected Model.Attribute Attribute { get; private set; } = null!;
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (AttributeId == Guid.Empty)
            {
                Attribute = new Model.Attribute();
                EditContext = new EditContext(Attribute);
                IsValid = true;

                return;
            }

            var result = await AttributeGateway.GetAttribute(AttributeId);

            if (!result.HasError())
            {
                Attribute = result.Success;
                EditContext = new EditContext(Attribute);
            }

            IsValid = !result.HasError();
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (AttributeId == Guid.Empty)
                await AttributeGateway.CreateAttribute(Attribute!);
            else
                await AttributeGateway.UpdateAttribute(AttributeId, Attribute!);

            NavigationManager.NavigateTo("attributes");
        }
    }
}