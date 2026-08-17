using Grpc.Core;
using grpc_csharp_server.Protos;

namespace grpc_csharp_server.Grpc
{

    public class ProductGrpcService : ProductService.ProductServiceBase
    {
        private static readonly List<ProductResponse> Products =
        [
            new ProductResponse
        {
            Id = 1,
            Name = "Laptop",
            Price = 1500        },
        new ProductResponse
        {
            Id = 2,
            Name = "Mouse",
            Price = 50        }
        ];

        public override Task<ProductResponse> GetProduct(
            GetProductRequest request,
            ServerCallContext context)
        {
            var product = Products.FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
            {
                throw new RpcException(
                    new Status(
                        StatusCode.NotFound,
                        "Product not found"));
            }

            return Task.FromResult(product);
        }

        public override Task<ProductListResponse> GetProducts(
            GetProductsRequest request,
            ServerCallContext context)
        {
            var response = new ProductListResponse();

            response.Products.AddRange(Products);

            return Task.FromResult(response);
        }

        public override Task<ProductResponse> CreateProduct(
            CreateProductRequest request,
            ServerCallContext context)
        {
            var product = new ProductResponse
            {
                Id = Products.Count == 0
                    ? 1
                    : Products.Max(x => x.Id) + 1,

                Name = request.Name,
                Price = request.Price,
            };

            Products.Add(product);

            return Task.FromResult(product);
        }

        public override Task<ProductResponse> UpdateProduct(
            UpdateProductRequest request,
            ServerCallContext context)
        {
            var product = Products.FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
            {
                throw new RpcException(
                    new Status(
                        StatusCode.NotFound,
                        "Product not found"));
            }

            product.Name = request.Name;
            product.Price = request.Price;

            return Task.FromResult(product);
        }

        public override Task<DeleteProductResponse> DeleteProduct(
            DeleteProductRequest request,
            ServerCallContext context)
        {
            var product = Products.FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
            {
                return Task.FromResult(new DeleteProductResponse
                {
                    Success = false
                });
            }

            Products.Remove(product);

            return Task.FromResult(new DeleteProductResponse
            {
                Success = true
            });
        }
    }
}