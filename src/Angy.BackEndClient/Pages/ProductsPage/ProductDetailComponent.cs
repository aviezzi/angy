using System;
using System.Threading.Tasks;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages.ProductsPage
{
    public class ProductDetailComponent : ComponentBase
    {
        [Parameter] public Guid ProductId { get; set; }

        protected ProductDetailViewModel ViewModel { get; private set; } = new ProductDetailViewModel();

        protected override async Task OnInitializedAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"query GetProductById($id: String) { product(id: $id) {name, description, microcategory {id, description} } microCategories { id, description}}",
                OperationName = "GetProductById",
                Variables = new
                {
                    id = ProductId
                }
            };

            using var client = new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer());

            var response = await client.SendQueryAsync<ProductDetailResponse>(query);

            var product = response.Data.Product;
            var microCategories = response.Data.MicroCategories;

            ViewModel = new ProductDetailViewModel(product, microCategories);
        }

        protected void HandleValidSubmit()
        {
            Console.WriteLine("OnValidSubmit");
        }
    }
}