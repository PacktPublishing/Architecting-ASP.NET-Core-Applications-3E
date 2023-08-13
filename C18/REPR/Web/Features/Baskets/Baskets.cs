using ForEvolve.ExceptionMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Web.Features;

public static partial class Baskets
{
    public record class BasketItem(int CustomerId, int ProductId, int Quantity);

    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> options)
            : base(options) { }

        public DbSet<BasketItem> Items => Set<BasketItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<BasketItem>()
                .HasKey(x => new { x.CustomerId, x.ProductId })
            ;
        }
    }

    public class DuplicateBasketItemException : ConflictException
    {
        public DuplicateBasketItemException(int productId)
            : base($"The product '{productId}' is already in your shopping cart.")
        {
        }
    }

    public class BasketItemNotFoundException : NotFoundException
    {
        public BasketItemNotFoundException(int productId)
            : base($"The product '{productId}' is not in your shopping cart.")
        {
        }
    }

    public static IServiceCollection AddBasketsFeature(this IServiceCollection services)
    {
        return services
            .AddAddItem()
            .AddFetchItems()
            .AddRemoveItem()
            .AddUpdateQuantity()
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
        group
            .MapFetchItems()
            .MapAddItem()
            .MapUpdateQuantity()
            .MapRemoveItem()
        ;
        return endpoints;
    }

    public static Task SeedBasketAsync(this IServiceScope scope)
    {
        return Task.CompletedTask;
    }
}
