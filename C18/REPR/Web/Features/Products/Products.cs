using ForEvolve.ExceptionMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Web.Features;

public static partial class Products
{
    public record class Product(string Name, int? Id = null);

    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }

    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int productId)
            : base($"The product '{productId}' was not found.")
        {
                
        }
    }

    internal static class ProductSeeder
    {
        public static Task SeedAsync(ProductContext db)
        {
            db.Products.Add(new Product(
                Name: "Banana",
                Id: 1
            ));
            db.Products.Add(new Product(
                Name: "Apple",
                Id: 2
            ));
            db.Products.Add(new Product(
                Name: "Habanero Pepper",
                Id: 3
            ));
            return db.SaveChangesAsync();
        }
    }

    public static IServiceCollection AddProductsFeature(this IServiceCollection services)
    {
        return services
            .AddFetchAll()
            .AddFetchOne()
            .AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("ProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            )
        ;
    }

    public static IEndpointRouteBuilder MapProductsFeature(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints
            .MapGroup(nameof(Products).ToLower())
            .WithTags(nameof(Products))
        ;
        group
            .MapFetchAll()
            .MapFetchOne()
        ;
        return endpoints;
    }

    public static async Task SeedProductsAsync(this IServiceScope scope)
    {
        var db = scope.ServiceProvider.GetRequiredService<ProductContext>();
        await ProductSeeder.SeedAsync(db);
    }
}
