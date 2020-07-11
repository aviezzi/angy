using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Shared.Model;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages.MicroCategoriesPage
{
    public class MicroCategoriesComponent : ComponentBase
    {
        [Inject]
        public GraphQLHttpClient HttpClient { get; set; }

        protected IEnumerable<MicroCategory> MicroCategories { get; private set; } = new List<MicroCategory>();

        protected override async Task OnInitializedAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"{ microcategories { id, name, description } }"
            };

            var response = await HttpClient.SendQueryAsync<MicroCategoriesResponse>(query);

            MicroCategories = response.Data.MicroCategories;
        }
    }
}