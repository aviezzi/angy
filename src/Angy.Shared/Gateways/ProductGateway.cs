using System.Threading.Tasks;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Http;

namespace Angy.Shared.Gateways
{
    public class ProductGateway
    {
        readonly GraphQLHttpClient _client;

        public ProductGateway(GraphQLHttpClient client)
        {
            _client = client;
        }

        public Task<ProductsResponse> GetProducts()
        {
            var query = new GraphQLRequest
            {
                Query = @"{ products { id, name, description, microcategory { name } } }"
            };

            return SendQueryAsync(query);
        }

        async Task<ProductsResponse> SendQueryAsync(GraphQLRequest query) => (await _client.SendQueryAsync<ProductsResponse>(query)).Data;
    }
}