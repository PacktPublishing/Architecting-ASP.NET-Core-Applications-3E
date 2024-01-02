namespace Shared.Contracts;
public record class ProductSummary(int Id, string Name);
public record class InsertProduct(string Name);
public record class UpdateProduct(int Id, string Name, int quantityInStock);
