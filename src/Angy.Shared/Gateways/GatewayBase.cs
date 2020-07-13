using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace Angy.Shared.Gateways
{
    public abstract class GatewayBase
    {
        readonly IGraphQLClient _client;

        protected GatewayBase(IGraphQLClient client)
        {
            _client = client;
        }

        protected async Task<TResponse> SendQueryAsync<TResponse>(GraphQLRequest request) => (await _client.SendQueryAsync<TResponse>(request)).Data;
    }
}