namespace VerticalApp.Features.Stocks;

public class NotEnoughStockException : Exception
{
    public NotEnoughStockException(int quantityInStock, int amountToRemove)
        : base($"You cannot remove {amountToRemove} item(s) when there is only {quantityInStock} item(s) left.")
    {
        QuantityInStock = quantityInStock;
        AmountToRemove = amountToRemove;
    }

    public int QuantityInStock { get; }
    public int AmountToRemove { get; }
}
