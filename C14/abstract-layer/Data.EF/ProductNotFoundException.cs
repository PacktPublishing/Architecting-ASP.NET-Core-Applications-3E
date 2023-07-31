namespace Data.EF;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int productId)
        : base($"The product '{productId}' was not found.")
    {
        ProductId = productId;
    }

    public int ProductId { get; }
}
