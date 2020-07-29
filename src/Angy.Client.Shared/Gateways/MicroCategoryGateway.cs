using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;

namespace Angy.Client.Shared.Gateways
{
    public class MicroCategoryGateway
    {
        readonly IClientAdapter _client;

        public MicroCategoryGateway(IClientAdapter client)
        {
            _client = client;
        }

        public Task<Result<IEnumerable<MicroCategory>, Error.ExceptionalError>> GetMicroCategoriesWithIdNameAndDescription()
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoriesResponse, IEnumerable<MicroCategory>>.Build(
                "{ categories { id, name, description } }",
                response => response.Categories!);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<IEnumerable<MicroCategory>, Error.ExceptionalError>> GetMicroCategoriesWithIdAndName()
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoriesResponse, IEnumerable<MicroCategory>>.Build("{ categories { id, name } }", response => response.Categories);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> GetMicroCategoryById(Guid id)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "query GetMicroCategoryById($id: String) { category(id: $id) {id, name, description } }",
                request => request.Category!,
                new { id },
                "GetMicroCategoryById"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> CreateMicroCategory(MicroCategory micro)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "mutation CreateMicroCategory($category: MicroCategoryInput!) { createCategory(category: $category) { id, name, description } }",
                response => response.Category!,
                new { category = SerializeMicro(micro) },
                "CreateMicroCategory"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<MicroCategory, Error.ExceptionalError>> UpdateMicroCategory(Guid id, MicroCategory micro)
        {
            var query = RequestAdapter<ResponsesAdapter.MicroCategoryResponse, MicroCategory>.Build(
                "mutation UpdateCategory($id: String!, $category: MicroCategoryInput!) { updateCategory(id: $id, category: $category) { id, name, description } }",
                response => response.Category,
                new { category = SerializeMicro(micro), id },
                "UpdateCategory"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeMicro(MicroCategory micro) => new
        {
            name = micro.Name,
            description = micro.Description
        };
    }
}