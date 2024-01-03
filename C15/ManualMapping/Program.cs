// Getting data/domain objects
var product = new Product(1, "Habanero pepper");
var inventories = new[] {
    new Inventory(1, "Warehouse West", 10),
    new Inventory(1, "Warehouse North", 15)
};

// Computing model into expected result
var quantityInStock = inventories.Sum(x => x.Quantity);

// Mapping to DTO
var dto = new ProductDetailsDto(product.Id, product.Name, quantityInStock);

// Using the DTO
Console.WriteLine($"ProductId: {dto.ProductId}");
Console.WriteLine($"ProductName: {dto.ProductName}");
Console.WriteLine($"ProductQuantityInStock: {dto.ProductQuantityInStock}");

// Data/domain model
public record class Product(int Id, string Name);
public record class Inventory(int ProductId, string Warehouse, int Quantity);

// DTO
public record class ProductDetailsDto(int ProductId, string ProductName, int ProductQuantityInStock);
