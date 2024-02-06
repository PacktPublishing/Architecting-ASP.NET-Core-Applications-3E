namespace Baskets.Features;

public class FetchItemsHandler
{
    private readonly BasketContext _db;
    private readonly FetchItemsMapper _mapper;

    public FetchItemsHandler(BasketContext db, FetchItemsMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<FetchItemsResponse> HandleAsync(FetchItemsQuery query, CancellationToken cancellationToken)
    {
        var items = _db.Items.Where(x => x.CustomerId == query.CustomerId);
        await items.LoadAsync(cancellationToken);
        var result = _mapper.Map(items);
        return result;
    }
}

