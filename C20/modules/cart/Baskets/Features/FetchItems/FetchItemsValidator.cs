namespace Baskets.Features;

public class FetchItemsValidator : AbstractValidator<FetchItemsQuery>
{
    public FetchItemsValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0);
    }
}

