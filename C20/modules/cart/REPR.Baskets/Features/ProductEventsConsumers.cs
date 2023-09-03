using REPR.Products.Contracts;

namespace REPR.Baskets.Features;

public class ProductEventsConsumers : IConsumer<ProductCreated>, IConsumer<ProductDeleted>
{
    private readonly BasketContext _db;
    private readonly ILogger _logger;
    public ProductEventsConsumers(BasketContext db, ILogger<ProductEventsConsumers> logger)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        _logger.LogTrace("Consuming {eventName} {eventId}", nameof(ProductCreated), context.MessageId);
        var product = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == context.Message.Id,
            cancellationToken: context.CancellationToken
        );
        if (product is not null)
        {
            _logger.LogWarning("The product {productId} already exist.", context.Message.Id);
            return;
        }
        _db.Products.Add(new(context.Message.Id));
        await _db.SaveChangesAsync();
        _logger.LogInformation("The product {productId} was created successfully.", context.Message.Id);
        _logger.LogTrace("Consumed {eventName} {eventId}", nameof(ProductCreated), context.MessageId);
    }

    public async Task Consume(ConsumeContext<ProductDeleted> context)
    {
        _logger.LogTrace("Consuming {eventName} {eventId}", nameof(ProductDeleted), context.MessageId);
        var item = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == context.Message.Id,
            cancellationToken: context.CancellationToken
        );
        if (item is null)
        {
            _logger.LogWarning("The product does not {productId} exist.", context.Message.Id);
            return;
        }

        // Remove the products from existing baskets
        var existingItemInCarts = _db.Items.Where(x => x.ProductId == context.Message.Id);
        var count = existingItemInCarts.Count();
        _db.Items.RemoveRange(existingItemInCarts);

        // Remove the product from the internal cache
        _db.Products.Remove(item);

        // Save the changes
        await _db.SaveChangesAsync();

        _logger.LogInformation(
            "The product {productId} was deleted successfully and removed from {count} basket(s).",
            context.Message.Id, count);
        _logger.LogTrace("Consumed {eventName} {eventId}", nameof(ProductDeleted), context.MessageId);
    }
}