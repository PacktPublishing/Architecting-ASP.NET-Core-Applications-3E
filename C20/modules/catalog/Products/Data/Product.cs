namespace Products.Data;

public record class Product(string Name, decimal UnitPrice, int? Id = null);
