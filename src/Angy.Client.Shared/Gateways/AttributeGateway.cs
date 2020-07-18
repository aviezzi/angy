using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;

namespace Angy.Client.Shared.Gateways
{
    public class AttributeGateway
    {
        readonly IClientAdapter _client;

        public AttributeGateway(IClientAdapter client)
        {
            _client = client;
        }

        public Task<Result<IEnumerable<Attribute>, Error.ExceptionalError>> GetAttributes()
        {
            var request = RequestAdapter<ResponsesAdapter.AttributesResponse, IEnumerable<Attribute>>.Build(
                "{ attributes { id, name } }",
                response => response.Attributes!
            );

            return _client.SendQueryAsync(request);
        }
    }
}