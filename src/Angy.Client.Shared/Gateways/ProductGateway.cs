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
                "{ products { id, name, microcategory { name } } }",
                response => response.Products!
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<(Product, IEnumerable<MicroCategory>), Error.ExceptionalError>> GetProductByIdWithMicroCategories(Guid id)
        {
            var request = RequestAdapter<ResponsesAdapter.ProductResponse, (Product, IEnumerable<MicroCategory>)>.Build(
                "query GetProductById($id: String) { product(id: $id) {id, name, microcategory { id, name } } microcategories { id, name}}",
                response => (response.Product!, response.MicroCategories!),
                new { id },
                "GetProductById"
            );

            return _client.SendQueryAsync(request);
        }

        public Task<Result<Product, Error.ExceptionalError>> CreateProduct(Product product)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation CreateProduct($product: ProductInput!) { createProduct(product: $product) { id, name, microcategory { id, description} } }",
                response => response.Product!,
                new { product = SerializeProduct(product) },
                "CreateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        public Task<Result<Product, Error.ExceptionalError>> UpdateProduct(Guid id, Product product)
        {
            var query = RequestAdapter<ResponsesAdapter.ProductResponse, Product>.Build(
                "mutation UpdateProduct($id: String!, $product: ProductInput!) { updateProduct(id: $id, product: $product) { id, name, microcategory { id, description} } }",
                response => response.Product!,
                new { product = SerializeProduct(product), id },
                "UpdateProduct"
            );

            return _client.SendQueryAsync(query);
        }

        static object SerializeProduct(Product product) => new
        {
            name = product.Name,
            microcategory = new
            {
                id = product.MicroCategory.Id
            }
        };
    }
}