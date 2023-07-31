using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    // Domain Layer
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IStockService, StockService>()

    // Data Layer (mapping Data.Abstract to Data.EF)
    .AddScoped<Data.Abstract.IProductRepository, Data.EF.ProductRepository>()
    .AddDbContext<Data.EF.ProductContext>(options => options
        .UseInMemoryDatabase("ProductContextMemoryDB")
        .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
    )
;

var app = builder.Build();

app.MapGet("/products", async (IProductService productService, CancellationToken cancellationToken) =>
{
    var products = await productService.AllAsync(cancellationToken);
    return products.Select(p => new
    {
        p.Id,
        p.Name,
        p.QuantityInStock
    });
});
app.MapPost("/products/{productId:int}/add-stocks", async (int productId, AddStocksCommand command, IStockService stockService, CancellationToken cancellationToken) =>
{
    try
    {
        var quantityInStock = await stockService.AddStockAsync(productId, command.Amount, cancellationToken);
        var stockLevel = new StockLevel(quantityInStock);
        return Results.Ok(stockLevel);
    }
    catch (ProductNotFoundException ex)
    {
        return Results.NotFound(new
        {
            ex.Message,
            productId,
        });
    }
});
app.MapPost("/products/{productId:int}/remove-stocks", async (int productId, RemoveStocksCommand command, IStockService stockService, CancellationToken cancellationToken) =>
{
    try
    {
        var quantityInStock = await stockService.RemoveStockAsync(productId, command.Amount, cancellationToken);
        var stockLevel = new StockLevel(quantityInStock);
        return Results.Ok(stockLevel);
    }
    catch (NotEnoughStockException ex)
    {
        return Results.Conflict(new
        {
            ex.Message,
            ex.AmountToRemove,
            ex.QuantityInStock
        });
    }
    catch (ProductNotFoundException ex)
    {
        return Results.NotFound(new
        {
            ex.Message,
            productId,
        });
    }
});

using (var seedScope = app.Services.CreateScope())
{
    var db = seedScope.ServiceProvider.GetRequiredService<Data.EF.ProductContext>();
    await ProductSeeder.SeedAsync(db);
}
app.Run();

internal class AddStocksCommand
{
    public int Amount { get; set; }
}

internal class RemoveStocksCommand
{
    public int Amount { get; set; }
}

internal class StockLevel
{
    public StockLevel(int quantityInStock)
    {
        QuantityInStock = quantityInStock;
    }

    public int QuantityInStock { get; set; }
}

internal static class ProductSeeder
{
    public static Task SeedAsync(Data.EF.ProductContext db)
    {
        db.Products.Add(new Product(
            id: 1,
            name: "Banana",
            quantityInStock: 50
        ));
        db.Products.Add(new Product(
            id: 2,
            name: "Apple",
            quantityInStock: 20
        ));
        db.Products.Add(new Product(
            id: 3,
            name: "Habanero Pepper",
            quantityInStock: 10
        ));
        return db.SaveChangesAsync();
    }
}