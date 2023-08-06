namespace VerticalApp.Models;

public class Product
{
    public Product(string name, int quantityInStock, int? id = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        QuantityInStock = quantityInStock;
        Id = id;
    }
    public int? Id { get; set; }
    public string Name { get; set; }
    public int QuantityInStock { get; set; }
}
