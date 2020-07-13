using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model.Model;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace Angy.Shared.Gateways
{
    public class MicroCategoryGateway : GatewayBase
    {
        protected MicroCategoryGateway(IGraphQLClient client) : base(client)
        {
        }

        public async Task<IEnumerable<MicroCategory>> GetMicroCategoriesWithIdAndName()
        {
            var query = new GraphQLRequest
            {
                Query = "{ microcategories { id, name } }"
            };

            return (await SendQueryAsync<MicroCategoriesResponse>(query)).MicroCategories;
        }

        public async Task<MicroCategory> GetMicroCategoryById(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @"query GetMicroCategoryById($id: String) { microcategory(id: $id) {id, name, description } }",
                OperationName = "GetMicroCategoryById",
                Variables = new
                {
                    id
                }
            };

            return (await SendQueryAsync<MicroCategoryResponse>(query)).MicroCategory;
        }

        public async Task<MicroCategory> CreateMicroCategory(MicroCategory micro)
        {
            var query = new GraphQLRequest
            {
                Query = @"mutation CreateMicroCategory($microcategory: MicroCategoryInput!) { createMicroCategory(microcategory: $microcategory) { id, name, description } }",
                OperationName = "CreateMicroCategory",
                Variables = new { microcategory = SerializeMicro(micro) }
            };

            return (await SendQueryAsync<MicroCategoryResponse>(query)).MicroCategory;
        }

        public async Task<MicroCategory> UpdateMicroCategory(Guid id, MicroCategory micro)
        {
            var query = new GraphQLRequest
            {
                Query = @"mutation UpdateMicroCategory($id: String!, $microcategory: MicroCategoryInput!) { updateMicroCategory(id: $id, microcategory: $microcategory) { id, name, description } }",
                OperationName = "UpdateMicroCategory",
                Variables = new { microcategory = SerializeMicro(micro), id }
            };

            return (await SendQueryAsync<MicroCategoryResponse>(query)).MicroCategory;
        }

        static object SerializeMicro(MicroCategory micro) => new
        {
            name = micro.Name,
            description = micro.Description
        };
    }
}