namespace REPR.Products.Features;

public class FetchOneProductHandler
{
    private readonly ProductContext _db;
    private readonly FetchOneProductMapper _mapper;

    public FetchOneProductHandler(ProductContext db, FetchOneProductMapper mapper)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<FetchOneProductResponse> HandleAsync(FetchOneProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == query.ProductId,
            cancellationToken: cancellationToken
        );
        if (product is null)
        {
            throw new ProductNotFoundException(query.ProductId);
        }
        return _mapper.Map(product);
    }
}
