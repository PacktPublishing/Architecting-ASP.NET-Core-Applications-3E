namespace Baskets.Contracts;

public record class UpdateQuantityCommand(int CustomerId, int ProductId, int Quantity);
