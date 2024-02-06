namespace Baskets.Features;

public class RemoveItemValidator : AbstractValidator<RemoveItemCommand>
{
    public RemoveItemValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}
