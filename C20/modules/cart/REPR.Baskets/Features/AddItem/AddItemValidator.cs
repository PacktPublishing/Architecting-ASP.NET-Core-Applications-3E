namespace REPR.Baskets.Features;

public class AddItemValidator : AbstractValidator<AddItemCommand>
{
    private readonly BasketContext _db;

    public AddItemValidator(BasketContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));

        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.ProductId)
            .GreaterThan(0)
            .MustAsync(ProductExistsAsync)
            .WithMessage("The Product does not exist.")
        ;
        RuleFor(x => x.Quantity).GreaterThan(0);
    }

    private async Task<bool> ProductExistsAsync(int productId, CancellationToken cancellationToken)
    {
        var product = await _db.Products
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);
        return product is not null;
    }
}
