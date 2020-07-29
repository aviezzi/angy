using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;

namespace Angy.Client.Shared.Gateways
{
    public class CategoryGateway
    {
        readonly IClientAdapter _client;

        public CategoryGateway(IClientAdapter client)
        {
            _client = client;
        }

        public Task<Result<IEnumerable<Category>, Error.ExceptionalError>> GetCategoriesWithIdNameAndDescription()
        {
            var query = RequestAdapter<ResponsesAdapter.CategoriesResponse, IEnumerable<Category>>.Build(
                "{ categories { id, name, description } }",
                response => response.Categories!);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<IEnumerable<Category>, Error.ExceptionalError>> GetCategoriesWithIdAndName()
        {
            var query = RequestAdapter<ResponsesAdapter.CategoriesResponse, IEnumerable<Category>>.Build("{ categories { id, name } }", response => response.Categories);

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Category, Error.ExceptionalError>> GetCategoryById(Guid id)
        {
            var query = RequestAdapter<ResponsesAdapter.CategoryResponse, Category>.Build(
                "query GetCategoryById($id: String) { category(id: $id) {id, name, description } }",
                request => request.Category!,
                new { id },
                "GetCategoryById"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Category, Error.ExceptionalError>> CreateCategory(Category category)
        {
            var query = RequestAdapter<ResponsesAdapter.CategoryResponse, Category>.Build(
                "mutation CreateCategory($category: CategoryInput!) { createCategory(category: $category) { id, name, description } }",
                response => response.Category!,
                new { category = SerializeMicro(category) },
                "CreateCategory"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Category, Error.ExceptionalError>> UpdateCategory(Guid id, Category category)
        {
            var query = RequestAdapter<ResponsesAdapter.CategoryResponse, Category>.Build(
                "mutation UpdateCategory($id: String!, $category: CategoryInput!) { updateCategory(id: $id, category: $category) { id, name, description } }",
                response => response.Category,
                new { category = SerializeMicro(category), id },
                "UpdateCategory"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeMicro(Category category) => new
        {
            name = category.Name,
            description = category.Description
        };
    }
}