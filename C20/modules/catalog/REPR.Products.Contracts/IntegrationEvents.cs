namespace REPR.Products.Contracts;

public record class ProductAdded(int Id, string Name, decimal UnitPrice);
public record class ProductDeleted(int Id);
