using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;
using Attribute = Angy.Model.Attribute;

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

        public Task<Result<Attribute, Error.ExceptionalError>> GetAttribute(Guid id)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Attribute>.Build(
                "query GetAttributeById($id: String) { attribute(id: $id) { id, name } }",
                response => response.Attribute!,
                new { id },
                "GetAttributeById"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Attribute, Error.ExceptionalError>> CreateAttribute(Attribute attribute)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Attribute>.Build(
                "mutation CreateAttribute($attribute: AttributeInput!) { createAttribute(attribute: $attribute) { id, name } }",
                response => response.Attribute!,
                new { attribute = SerializeAttribute(attribute) },
                "CreateAttribute"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Attribute, Error.ExceptionalError>> UpdateAttribute(Guid id, Attribute attribute)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Attribute>.Build(
                "mutation UpdateAttribute($id: String!, $attribute: AttributeInput!) { updateAttribute(id: $id, attribute: $attribute) { id, name } }",
                response => response.Attribute!,
                new { id, attribute = new { name = attribute.Name } },
                "UpdateAttribute"
            );

            return _client.SendQueryAsync(request);
        }

        static object SerializeAttribute(Attribute attribute) => new { name = attribute.Name };
    }
}