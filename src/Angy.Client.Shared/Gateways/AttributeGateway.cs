using System;
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

        public Task<Result<IEnumerable<Model.Attribute>, Error.ExceptionalError>> GetAttributes()
        {
            var request = RequestAdapter<ResponsesAdapter.AttributesResponse, IEnumerable<Model.Attribute>>.Build(
                "{ attributes { id, name } }",
                response => response.Attributes!
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Model.Attribute, Error.ExceptionalError>> GetAttribute(Guid id)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Model.Attribute>.Build(
                "query GetAttributeById($id: String) { attribute(id: $id) { id, name } }",
                response => response.Attribute!,
                new { id },
                "GetAttributeById"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Model.Attribute, Error.ExceptionalError>> CreateAttribute(Model.Attribute attribute)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Model.Attribute>.Build(
                "mutation CreateAttribute($attribute: AttributeInput!) { createAttribute(attribute: $attribute) { id } }",
                response => response.Attribute!,
                new { attribute = SerializeAttribute(attribute) },
                "CreateAttribute"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Model.Attribute, Error.ExceptionalError>> UpdateAttribute(Guid id, Model.Attribute attribute)
        {
            var request = RequestAdapter<ResponsesAdapter.AttributeResponse, Model.Attribute>.Build(
                "mutation UpdateAttribute($attribute: AttributeInput!) { updateAttribute(attribute: $attribute) { id } }",
                response => response.Attribute!,
                new { attribute = SerializeAttribute(attribute) },
                "UpdateAttribute"
            );

            return _client.SendQueryAsync(request);
        }

        static object SerializeAttribute(Model.Attribute attribute) => new
        {
            id = attribute.Id,
            name = attribute.Name
        };
    }
}