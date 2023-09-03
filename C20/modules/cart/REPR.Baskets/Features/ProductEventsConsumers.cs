using MassTransit;
using Microsoft.EntityFrameworkCore;
using REPR.Baskets.Data;
using REPR.Products.Contracts;
using System;
using System.Threading;

namespace REPR.Baskets.Features;

public class ProductEventsConsumers : IConsumer<ProductAdded>, IConsumer<ProductDeleted>
{
    private readonly BasketContext _db;
    public ProductEventsConsumers(BasketContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task Consume(ConsumeContext<ProductAdded> context)
    {
        var item = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == context.Message.Id,
            cancellationToken: context.CancellationToken
        );
        if (item is not null)
        {
            return;
        }
        _db.Products.Add(new(context.Message.Id));
        await _db.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<ProductDeleted> context)
    {
        var item = await _db.Products.FirstOrDefaultAsync(
            x => x.Id == context.Message.Id,
            cancellationToken: context.CancellationToken
        );
        if (item is null)
        {
            return;
        }

        // Remove the products from existing baskets
        var existingItemInCarts = _db.Items.Where(x => x.ProductId == context.Message.Id);
        _db.Items.RemoveRange(existingItemInCarts);

        // Remove the product from the internal cache
        _db.Products.Remove(item);

        // Save the changes
        await _db.SaveChangesAsync();
    }
}