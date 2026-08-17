gRPC Product Service

A simple gRPC-based Product Service built with C# and .NET 10.

The solution contains a gRPC server implemented with ASP.NET Core and a Console application that consumes the gRPC service.

Projects

grpc-csharp
│
├── grpc_csharp_server
│   ├── Grpc
│   │   └── ProductGrpcService.cs
│   ├── Protos
│   │   └── product.proto
│   └── Program.cs
│
└── grpc_csharp_client
    ├── Protos
    │   └── product.proto
    └── Program.cs

Features

The Product gRPC service provides the following operations:

* Get a product
* Get all products
* Create a product
* Update a product
* Delete a product

gRPC Service

The service is defined in product.proto:

service ProductService {
  rpc GetProduct (GetProductRequest) returns (ProductResponse);
  rpc GetProducts (GetProductsRequest) returns (ProductListResponse);
  rpc CreateProduct (CreateProductRequest) returns (ProductResponse);
  rpc UpdateProduct (UpdateProductRequest) returns (ProductResponse);
  rpc DeleteProduct (DeleteProductRequest) returns (DeleteProductResponse);
}


Technologies

* C#
* .NET 10
* ASP.NET Core
* gRPC
* Protocol Buffers
* HTTP/2

Running the Server

Go to the server project:

cd grpc_csharp_server


Run the application:

dotnet run


The server will start on the configured HTTP/HTTPS ports.

Check Properties/launchSettings.json to find the exact server URL.

## Running the Client

Open a second terminal and go to the client project:


cd grpc_csharp_client


Run:


dotnet run


Make sure the URL configured in the client matches the URL of the gRPC server:

using var channel =
    GrpcChannel.ForAddress("https://localhost:7185");


Example

The Console Client can call the gRPC service:

var client = new ProductService.ProductServiceClient(channel);

var product = await client.GetProductAsync(
    new GetProductRequest
    {
        Id = 1
    });

Console.WriteLine($"Id: {product.Id}");
Console.WriteLine($"Name: {product.Name}");
Console.WriteLine($"Price: {product.Price}");


Development Certificate

If HTTPS certificate errors occur during local development, run:

dotnet dev-certs https --trust


Then restart the server and client.

## Notes

The current implementation uses an in-memory List<ProductResponse> as the data store.

For a production application, this can be replaced with:

* Entity Framework Core
* SQL Server
* PostgreSQL
* Repository/Service layers
* Dependency Injection
* Authentication and Authorization

License

This project is for learning and demonstration purposes.
