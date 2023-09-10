namespace REPR.Products.Features;

public class DeleteProductHandler
{
    private readonly ProductContext _db;
    private readonly DeleteProductMapper _mapper;
    private readonly IBus _bus;

    public DeleteProductHandler(ProductContext db, DeleteProductMapper mapper, IBus bus)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public async Task<DeleteProductResponse> HandleAsync(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == command.ProductId,
            cancellationToken: cancellationToken
        );
        if (product is null)
        {
            throw new ProductNotFoundException(command.ProductId);
        }
        _db.Products.Remove(product);
        await _db.SaveChangesAsync(cancellationToken);

        var productDeleted = _mapper.MapToIntegrationEvent(product);
        await _bus.Publish(productDeleted, CancellationToken.None);

        var result = _mapper.MapToResponse(product);
        return result;
    }
}
