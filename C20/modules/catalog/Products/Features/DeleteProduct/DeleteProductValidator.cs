namespace Products.Features;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
    }
}
