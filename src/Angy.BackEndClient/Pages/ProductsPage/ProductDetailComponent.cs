using System;
using System.Threading.Tasks;
using Angy.Shared.Model;
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

        protected Product Product { get; private set; } = new Product();

        protected override async Task OnInitializedAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"query GetProductById($id: String) { product(id: $id) {name, description, microcategory {id, description} }}",
                OperationName = "GetProductById",
                Variables = new
                {
                    id = ProductId
                }
            };

            using var client = new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer());

            var response = await client.SendQueryAsync<ProductResponse>(query);

            Product = response.Data.Product;
        }

        protected void HandleValidSubmit()
        {
            Console.WriteLine("OnValidSubmit");
        }
    }
}