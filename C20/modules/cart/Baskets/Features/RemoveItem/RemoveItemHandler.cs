namespace Baskets.Features;

public class RemoveItemHandler
{
    private readonly BasketContext _db;
    private readonly RemoveItemMapper _mapper;

    public RemoveItemHandler(BasketContext db, RemoveItemMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<RemoveItemResponse> HandleAsync(RemoveItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _db.Items.FirstOrDefaultAsync(
            x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
            cancellationToken: cancellationToken
        );
        if (item is null)
        {
            throw new BasketItemNotFoundException(command.ProductId);
        }
        _db.Items.Remove(item);
        await _db.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map(item);
        return result;
    }
}
