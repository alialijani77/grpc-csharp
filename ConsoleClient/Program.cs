using Grpc.Net.Client;
using grpc_csharp_server.Protos;

using var channel = GrpcChannel.ForAddress("https://localhost:7167");

var client = new ProductService.ProductServiceClient(channel);

// -----------------------------
// GetProduct
// -----------------------------

var product = await client.GetProductAsync(
    new GetProductRequest
    {
        Id = 1
    });

Console.WriteLine("Product:");
Console.WriteLine($"Id: {product.Id}");
Console.WriteLine($"Name: {product.Name}");
Console.WriteLine($"Price: {product.Price}");


// -----------------------------
// GetProducts
// -----------------------------

var products = await client.GetProductsAsync(
    new GetProductsRequest());

Console.WriteLine();
Console.WriteLine("All Products:");

foreach (var item in products.Products)
{
    Console.WriteLine(
        $"{item.Id} - {item.Name} - {item.Price}");
}

Console.ReadLine();
