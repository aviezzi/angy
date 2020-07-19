using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;

namespace Angy.Client.ProductDataManager.Pages.AttributesPage
{
    public class AttributesComponent : ComponentBase
    {
        [Inject] public AttributeGateway AttributeGateway { get; set; } = null!;

        protected IEnumerable<Attribute> Attributes { get; private set; } = new List<Attribute>();
        protected bool? IsValid { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await AttributeGateway.GetAttributes();

            IsValid = result.IsValid;

            if (result.IsValid) Attributes = result.Success;
        }
    }
}