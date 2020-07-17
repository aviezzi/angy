using System.Threading.Tasks;
using Angy.Model;
using Angy.Shared.Abstract;
using GraphQL.Client.Abstractions;

namespace Angy.Shared.Adapters
{
    public class ClientAdapter : IClientAdapter
    {
        readonly IGraphQLClient _client;

        public ClientAdapter(IGraphQLClient client)
        {
            _client = client;
        }

        public Task<Result<TCast, Error.ExceptionalError>> SendQueryAsync<TResponse, TCast>(RequestAdapter<TResponse, TCast> adapter) =>
            Result.Try(async () =>
            {
                var response = (await _client.SendQueryAsync<TResponse>(adapter.Request)).Data;
                return adapter.Selector(response);
            });
    }
}