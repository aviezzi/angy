using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Client.Shared.Abstract;
using Angy.Client.Shared.Adapters;
using Angy.Client.Shared.Responses;
using Angy.Model;

namespace Angy.Client.Shared.Gateways
{
    public class ProductGateway
    {
        readonly IClientAdapter _client;

        public ProductGateway(IClientAdapter client)
        {
            _client = client;
        }

        public Task<Result<IEnumerable<Product>, Error.ExceptionalError>> GetProductsWithIdNameDescriptionAndMicroName()
        {
            var request = RequestAdapter<ResponsesAdapter.ProductsResponse, IEnumerable<Product>>.Build(
                "{ products { id, name, category { name } } }",
                response => response.Products!
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<(Product, IEnumerable<Category>), Error.ExceptionalError>> GetProductByIdWithCategories(Guid id)
        {
            var request = RequestAdapter<ResponsesAdapter.ProductResponse, (Product, IEnumerable<Category>)>.Build(
                "query GetProductById($id: String) { product(id: $id) {id, name, categoryId } categories { id, name}}",
                response => (response.Product!, response.Categories!),
                new { id },
                "GetProductById"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Product, Error.ExceptionalError>> CreateProduct(Product product)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation CreateProduct($product: ProductInput!) { createProduct(product: $product) { id, name, category { id, description} } }",
                response => response.Product!,
                new { product = SerializeProduct(product) },
                "CreateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Product, Error.ExceptionalError>> UpdateProduct(Product product, Guid productId)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation UpdateProduct($id: String!, $product: ProductInput!) { updateProduct(id: $id, product: $product) { id, name, category { id, description} } }",
                response => response.Product!,
                new { product = SerializeProduct(product), id = productId },
                "UpdateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeProduct(Product product) => new
        {
            name = product.Name,
            categoryId = product.CategoryId
        };
    }
}