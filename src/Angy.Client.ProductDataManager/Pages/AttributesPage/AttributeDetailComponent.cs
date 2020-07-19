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

        protected EditContext EditContext = new EditContext(new Model.Attribute());
        protected Result<Model.Attribute, Error.ExceptionalError>? Result { get; private set; } 

        protected override async Task OnInitializedAsync()
        {
            Result = await AttributeGateway.GetAttribute(AttributeId);

            if (Result.IsValid == true) EditContext = new EditContext(Result.Success);
        }

        protected Task HandleSubmit() => null;
    }
}