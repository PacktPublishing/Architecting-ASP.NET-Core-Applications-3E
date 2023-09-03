namespace REPR.Baskets.Features;

public class UpdateQuantityHandler
{
    private readonly BasketContext _db;
    private readonly UpdateQuantityMapper _mapper;

    public UpdateQuantityHandler(BasketContext db, UpdateQuantityMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<UpdateQuantityResponse> HandleAsync(UpdateQuantityCommand command, CancellationToken cancellationToken)
    {
        var item = await _db.Items.AsNoTracking().FirstOrDefaultAsync(
            x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
            cancellationToken: cancellationToken
        );
        if (item is null)
        {
            throw new BasketItemNotFoundException(command.ProductId);
        }
        var itemToUpdate = item with { Quantity = command.Quantity };
        if (item.Quantity != command.Quantity)
        {
            _db.Items.Update(itemToUpdate);
            await _db.SaveChangesAsync(cancellationToken);
        }
        var result = _mapper.Map(itemToUpdate);
        return result;
    }
}
