using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<Result<IEnumerable<Product>, Error.Exceptional>> GetProductsWithIdNameDescriptionAndMicroName()
        {
            var request = RequestAdapter<ResponsesAdapter.ProductsResponse, IEnumerable<Product>>.Build(
                "{ products { id, name, category { name } } }",
                response => response.Products!
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<(Product, IEnumerable<Category>, IEnumerable<Model.Attribute>), Error.Exceptional>> GetProductByIdWithCategoriesAndAttributes(Guid id)
        {
            var request = RequestAdapter<ResponsesAdapter.ProductResponse, (Product, IEnumerable<Category>, IEnumerable<Model.Attribute>)>.Build(
                "query GetProductById($id: String) { product(id: $id) {id, name, categoryId, descriptions { id, description, attributeId, attribute { id, name } } } categories { id, name } attributes { id, name } }",
                response => (response.Product!, response.Categories!, response.Attributes.Except(response.Product!.Descriptions.Select(desc => desc.Attribute!)!)!),
                new { id },
                "GetProductById"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<(IEnumerable<Category>, IEnumerable<Model.Attribute>), Error.Exceptional>> GetCategoriesAndAttributes()
        {
            var query = RequestAdapter<ResponsesAdapter.CategoriesAttributesResponse, (IEnumerable<Category>, IEnumerable<Model.Attribute>)>.Build(
                "{ categories { id, name } attributes { id, name} }",
                response => (response.Categories, response.Attributes));

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Product, Error.Exceptional>> CreateProduct(Product product)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation CreateProduct($product: ProductInput!) { createProduct(product: $product) { id } }",
                response => response.Product!,
                new { product = SerializeProduct(product) },
                "CreateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Product, Error.Exceptional>> UpdateProduct(Product product, Guid productId)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation UpdateProduct($product: ProductInput!) { updateProduct(product: $product) { id } }",
                response => response.Product!,
                new { product = SerializeProduct(product) },
                "UpdateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeProduct(Product product) => new
        {
            id = product.Id,
            name = product.Name,
            categoryId = product.CategoryId,
            descriptions = product.Descriptions.Select(description => new
            {
                id = description.Id,
                description = description.Description,
                attributeId = description.AttributeId,
                productId = product.Id
            })
        };
    }
}