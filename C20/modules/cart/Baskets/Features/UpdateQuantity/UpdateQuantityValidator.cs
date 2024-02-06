namespace Baskets.Features;

public class UpdateQuantityValidator : AbstractValidator<UpdateQuantityCommand>
{
    public UpdateQuantityValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
