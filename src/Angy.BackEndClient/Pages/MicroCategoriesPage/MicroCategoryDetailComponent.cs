using System;
using System.Threading.Tasks;
using Angy.Shared.Responses;
using Angy.Shared.ViewModels;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Components;

namespace Angy.BackEndClient.Pages.MicroCategoriesPage
{
    public class MicroCategoryDetailComponent : ComponentBase
    {
        [Inject]
        public GraphQLHttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Guid MicroId { get; set; }

        protected MicroCategoryViewModel ViewModel { get; private set; } = new MicroCategoryViewModel();

        protected override async Task OnInitializedAsync()
        {
            if (MicroId != Guid.Empty)
            {
                var query = new GraphQLRequest
                {
                    Query = @"query GetMicroCategoryById($id: String) { microcategory(id: $id) {id, name, description } }",
                    OperationName = "GetMicroCategoryById",
                    Variables = new
                    {
                        id = MicroId
                    }
                };

                var response = await HttpClient.SendQueryAsync<MicroCategoryResponse>(query);

                var micro = response.Data.MicroCategory;

                ViewModel = new MicroCategoryViewModel(micro);
            }
        }

        protected async Task HandleValidSubmit()
        {
            var createQuery = new GraphQLRequest
            {
                Query = @"mutation CreateMicroCategory($microcategory: MicroCategoryInput!) { createMicroCategory(microcategory: $microcategory) { id, name, description } }",
                OperationName = "CreateMicroCategory",
                Variables = new
                {
                    microcategory = new
                    {
                        name = ViewModel.Name,
                        description = ViewModel.Description
                    }
                }
            };

            var updateQuery = new GraphQLRequest
            {
                Query = @"mutation UpdateMicroCategory($id: String!, $microcategory: MicroCategoryInput!) { updateMicroCategory(id: $id, microcategory: $microcategory) { id, name, description } }",
                OperationName = "UpdateMicroCategory",
                Variables = new
                {
                    microcategory = new
                    {
                        name = ViewModel.Name,
                        description = ViewModel.Description
                    },
                    id = ViewModel.Micro.Id
                }
            };

            var query = ViewModel.Micro.Id == Guid.Empty ? createQuery : updateQuery;

            var micro = await HttpClient.SendQueryAsync<MicroCategoryResponse>(query);

            NavigationManager.NavigateTo("/micro-categories");
        }
    }
}