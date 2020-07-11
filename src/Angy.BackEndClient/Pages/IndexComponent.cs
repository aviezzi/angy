using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model.Model;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages
{
    public class IndexComponent : ComponentBase
    {
        [Inject]
        public GraphQLHttpClient HttpClient { get; set; }

        protected IEnumerable<Product> Products { get; private set; } = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"{ products { id, name, description, microcategory { description } } }"
            };

            var response = await HttpClient.SendQueryAsync<ProductsResponse>(query);

            Products = response.Data.Products;
        }
    }
}