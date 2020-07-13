using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model.Model;
using Angy.Shared.Responses;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace Angy.Shared.Gateways
{
    public class ProductGateway : GatewayBase
    {
        protected ProductGateway(IGraphQLClient client) : base(client)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsWithIdNameDescriptionAndMicroName()
        {
            var query = new GraphQLRequest
            {
                Query = @"{ products { id, name, description, microcategory { name } } }"
            };

            var response = await SendQueryAsync<ProductsResponse>(query);

            return response.Products;
        }

        public async Task<(Product Product, IEnumerable<MicroCategory> MicroCategories)> GetProductByIdWithMicroCategories(Guid productId)
        {
            var query = new GraphQLRequest
            {
                Query = @"query GetProductById($id: String) { product(id: $id) {id, name, description, microcategory { id, name } } microcategories { id, name}}",
                OperationName = "GetProductById",
                Variables = new
                {
                    id = productId
                }
            };

            var response = await SendQueryAsync<ProductResponse>(query);

            return (response.Product, response.MicroCategories);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            var query = new GraphQLRequest
            {
                Query = @"mutation CreateProduct($product: ProductInput!) { createProduct(product: $product) { id, name, description, microcategory { id, description} } }",
                OperationName = "CreateProduct",
                Variables = new { product = SerializeProduct(product) }
            };

            var response = await SendQueryAsync<ProductResponse>(query);

            return response.Product;
        }

        public async Task<Product> UpdateProduct(Guid id, Product product)
        {
            var query = new GraphQLRequest
            {
                Query = @"mutation UpdateProduct($id: String!, $product: ProductInput!) { updateProduct(id: $id, product: $product) { id, name, description, microcategory { id, description} } }",
                OperationName = "UpdateProduct",
                Variables = new { product = SerializeProduct(product), id }
            };

            var response = await SendQueryAsync<ProductResponse>(query);

            return response.Product;
        }

        static object SerializeProduct(Product product) => new
        {
            name = product.Name,
            description = product.Description,
            microcategory = new
            {
                id = product.MicroCategory.Id
            }
        };
    }
}