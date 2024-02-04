namespace Baskets.Features;

public class AddItemHandler
{
    private readonly BasketContext _db;
    private readonly AddItemMapper _mapper;

    public AddItemHandler(BasketContext db, AddItemMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AddItemResponse> HandleAsync(AddItemCommand command, CancellationToken cancellationToken)
    {
        var itemExists = await _db.Items.AnyAsync(
            x => x.CustomerId == command.CustomerId && x.ProductId == command.ProductId,
            cancellationToken: cancellationToken
        );
        if (itemExists)
        {
            throw new DuplicateBasketItemException(command.ProductId);
        }
        var item = _mapper.Map(command);
        _db.Add(item);
        await _db.SaveChangesAsync(cancellationToken);
        var result = _mapper.Map(item);
        return result;
    }
}
