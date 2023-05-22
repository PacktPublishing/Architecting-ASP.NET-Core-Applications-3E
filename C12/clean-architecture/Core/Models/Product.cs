namespace Core.Models;
public class Product
{
    public Product(string name, int quantityInStock, int? id = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        QuantityInStock = quantityInStock;
        Id = id;
    }

    public int? Id { get; init; }
    public string Name { get; init; }
    public int QuantityInStock { get; private set; }

    public void AddStock(int amount)
    {
        if (amount == 0) { return; }
        if (amount < 0) { throw new NegativeValueException(amount); }
        QuantityInStock += amount;
    }

    public void RemoveStock(int amount)
    {
        if (amount == 0) { return; }
        if (amount < 0) { throw new NegativeValueException(amount); }
        if (amount > QuantityInStock) { throw new NotEnoughStockException(QuantityInStock, amount); }
        QuantityInStock -= amount;
    }
}
