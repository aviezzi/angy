using System;
using System.Threading.Tasks;
using Angy.Shared.Responses;
using Angy.Shared.ViewModels;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages.ProductsPage
{
    public class ProductDetailComponent : ComponentBase
    {
        [Inject]
        public GraphQLHttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Guid ProductId { get; set; }

        protected ProductViewModel ViewModel { get; private set; } = new ProductViewModel();

        protected override async Task OnInitializedAsync()
        {
            if (ProductId == Guid.Empty)
            {
                var query = new GraphQLRequest
                {
                    Query = "{ microcategories { id, description } }"
                };

                var response = await HttpClient.SendQueryAsync<ProductResponse>(query);

                var microCategories = response.Data.MicroCategories;

                ViewModel = new ProductViewModel(microCategories);
            }
            else
            {
                var query = new GraphQLRequest
                {
                    Query = @"query GetProductById($id: String) { product(id: $id) {id, name, description, microcategory {id, description} } microcategories { id, description}}",
                    OperationName = "GetProductById",
                    Variables = new
                    {
                        id = ProductId
                    }
                };

                var response = await HttpClient.SendQueryAsync<ProductResponse>(query);

                var product = response.Data.Product;
                var microCategories = response.Data.MicroCategories;

                ViewModel = new ProductViewModel(product, microCategories);
            }
        }

        protected async Task HandleValidSubmit()
        {
            var createQuery = new GraphQLRequest
            {
                Query = @"mutation CreateProduct($product: ProductInput!) { createProduct(product: $product) { id, name, description, microcategory { id, description} } }",
                OperationName = "CreateProduct",
                Variables = new
                {
                    product = ViewModel.Product
                }
            };

            var updateQuery = new GraphQLRequest
            {
                Query = @"mutation UpdateProduct($id: String!, $product: ProductInput!) { updateProduct(id: $id, product: $product) { id, name, description, microcategory { id, description} } }",
                OperationName = "UpdateProduct",
                Variables = new
                {
                    product = ViewModel.Product,
                    id = ViewModel.Product.Id
                }
            };

            var query = ViewModel.Product.Id == Guid.Empty ? createQuery : updateQuery;

            await HttpClient.SendQueryAsync<ProductResponse>(query);

            NavigationManager.NavigateTo("products");
        }
    }
}