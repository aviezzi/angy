using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Model;
using GraphQL.Client.Abstractions;

namespace Angy.Client.Shared.Adapters
{
    public class ClientAdapter : IClientAdapter
    {
        readonly IGraphQLClient _client;

        public ClientAdapter(IGraphQLClient client)
        {
            _client = client;
        }

        public Task<Result<TCast, Error.Exceptional>> SendQueryAsync<TResponse, TCast>(RequestAdapter<TResponse, TCast> adapter) =>
            Result.Try(async () =>
            {
                var response = (await _client.SendQueryAsync<TResponse>(adapter.Request)).Data;
                return adapter.Selector(response);
            });
    }
}