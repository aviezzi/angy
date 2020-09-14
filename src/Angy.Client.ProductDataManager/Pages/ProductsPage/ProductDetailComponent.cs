using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angy.Client.Shared.Gateways;
using Angy.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.ProductDataManager.Pages.ProductsPage
{
    public class ProductDetailComponent : ComponentBase
    {
        [Parameter] public Guid ProductId { get; set; }

        [Inject] public ProductGateway ProductGateway { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected EditContext EditContext { get; private set; } = null!;
        protected Product Product { get; private set; } = null!;
        protected IEnumerable<Category> Categories { get; private set; } = null!;
        protected ICollection<Model.Attribute> Attributes { get; private set; } = null!;
        protected Guid SelectedAttribute { get; set; }
        protected string Filter { get; set; } = string.Empty;

        protected bool? IsValid { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            if (ProductId == Guid.Empty)
                await InitializeCreateAsync();
            else
                await InitializeUpdateAsync();
        }

        protected void Add(Model.Attribute toMove)
        {
            Product.Descriptions.Add(new AttributeDescription(string.Empty, attributeId: toMove.Id, attribute: toMove));
            Attributes.Remove(toMove);
            EditContext.NotifyFieldChanged(FieldIdentifier.Create(() => Product.Descriptions));
        }

        protected void Remove(AttributeDescription toMove)
        {
            Attributes.Add(toMove.Attribute);
            Product.Descriptions.Remove(toMove);
            EditContext.NotifyFieldChanged(FieldIdentifier.Create(() => Product.Descriptions));
        }

        protected async Task HandleSubmit()
        {
            if (!EditContext.Validate()) return;

            if (ProductId == Guid.Empty)
                await ProductGateway.CreateProduct(Product);
            else
                await ProductGateway.UpdateProduct(Product, ProductId);

            NavigationManager.NavigateTo("products");
        }

        async Task InitializeUpdateAsync()
        {
            var result = await ProductGateway.GetProductByIdWithCategoriesAndAttributes(ProductId);

            if (!result.HasError())
            {
                var (product, categories, attributes) = result.Success;

                Categories = categories;
                Attributes = attributes.ToList();

                Product = product;
                EditContext = new EditContext(Product);
            }

            IsValid = !result.HasError();
        }

        async Task InitializeCreateAsync()
        {
            var result = await ProductGateway.GetCategoriesAndAttributes();

            if (!result.HasError())
            {
                var (categories, attributes) = result.Success;

                Categories = categories;
                Attributes = attributes.ToList();

                Product = new Product();
                EditContext = new EditContext(Product);
            }

            IsValid = !result.HasError();
        }
    }
}