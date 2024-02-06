namespace Baskets.Contracts;

public record class RemoveItemCommand(int CustomerId, int ProductId);
