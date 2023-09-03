namespace REPR.Baskets.Contracts;

public record class AddItemCommand(
    int CustomerId,
    int ProductId,
    int Quantity
);
