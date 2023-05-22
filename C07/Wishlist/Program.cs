using Microsoft.Extensions.Options;
using Wishlist;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .ConfigureOptions<InMemoryWishListOptions>()
    .AddTransient<IValidateOptions<InMemoryWishListOptions>, InMemoryWishListOptions>()
    .AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<InMemoryWishListOptions>>().Value)

    .AddSingleton<IWishList, InMemoryWishList>()

    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
;

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", async (IWishList wishList) => await wishList.AllAsync());
app.MapPost("/", async (IWishList wishList, CreateItem? newItem) =>
{
    if (newItem?.Name == null)
    {
        return Results.BadRequest();
    }
    var item = await wishList.AddOrRefreshAsync(newItem.Name);
    return Results.Created("/", item);
}).Produces(201, typeof(WishListItem)).Produces(400);
app.Run();

public record class CreateItem(string? Name);
