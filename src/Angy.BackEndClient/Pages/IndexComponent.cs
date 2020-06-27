using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Shared.Model;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages
{
    public class IndexComponent : ComponentBase
    {
        protected IEnumerable<Product> Products { get; private set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"{ products { id, name, description } }"
            };

            using var client = new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer());

            var response = await client.SendQueryAsync<ProductsResponse>(query);

            Products = response.Data.Products;
        }
    }
}