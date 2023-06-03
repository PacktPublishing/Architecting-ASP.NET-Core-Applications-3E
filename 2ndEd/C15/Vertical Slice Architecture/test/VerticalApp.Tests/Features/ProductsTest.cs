using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VerticalApp.Data;
using VerticalApp.Models;
using Xunit;

namespace VerticalApp.Features.Products;

public class ProductsTest
{
    private static async Task SeederDelegate(ProductContext db)
    {
        db.Products.RemoveRange(db.Products.ToArray());
        await db.Products.AddAsync(new Product(
            id: 1,
            name: "Banana",
            quantityInStock: 50
        ));
        await db.Products.AddAsync(new Product(
            id: 2,
            name: "Scotch Bottle",
            quantityInStock: 20
        ));
        await db.Products.AddAsync(new Product(
            id: 3,
            name: "Habanero Pepper",
            quantityInStock: 10
        ));
        await db.SaveChangesAsync();
    }

    public class ListAllProductsTest : ProductsTest
    {
        [Fact]
        public async Task Should_return_all_products()
        {
            // Arrange
            await using var application = new VerticalAppApplication();
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(new ListAllProducts.Command());

            // Assert
            using var assertScope = application.Services.CreateScope();
            var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            Assert.Collection(result,
                product => Assert.Equal("Banana", product.Name),
                product => Assert.Equal("Scotch Bottle", product.Name),
                product => Assert.Equal("Habanero Pepper", product.Name)
            );
        }
    }
}
