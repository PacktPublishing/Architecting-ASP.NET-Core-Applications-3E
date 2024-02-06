namespace Products.Contracts;

public record class CreateProductCommand(string Name, decimal UnitPrice);
