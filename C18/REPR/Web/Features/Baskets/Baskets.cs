using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Web.Features;

public static partial class Baskets
{
    public record class ShoppingCart(int CustomerId)
    {
        public List<Item> Items { get; } = new();
    }
    public record class Item(int ProductId, int Quantity);

    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options) { }

        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    }

    public static IServiceCollection AddBasketsFeature(this IServiceCollection services)
    {
        return services
            //
            // TODO: Register endpoints services
            //
            .AddDbContext<BasketContext>(options => options
                .UseInMemoryDatabase("BasketContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            )
        ;
    }

    public static IEndpointRouteBuilder MapBasketsFeature(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(nameof(Baskets).ToLower())
            .WithTags(nameof(Baskets))
        ;
        //group
        //    // TODO: Register endpoints maps
        //;
        return endpoints;
    }

    public static Task SeedBasketAsync(this IServiceScope scope)
    {
        return Task.CompletedTask;
    }
}
