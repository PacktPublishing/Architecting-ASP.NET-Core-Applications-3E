namespace Products.Features;

public class FetchAllProductsHandler
{
    private readonly ProductContext _db;
    private readonly FetchAllProductsMapper _mapper;

    public FetchAllProductsHandler(ProductContext db, FetchAllProductsMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<FetchAllProductsResponse> HandleAsync(FetchAllProductsQuery query, CancellationToken cancellationToken)
    {
        await _db.Products.LoadAsync(cancellationToken);
        var products = _mapper.Project(_db.Products.OrderBy(x => x.Name));
        return new FetchAllProductsResponse(products);
    }
}
