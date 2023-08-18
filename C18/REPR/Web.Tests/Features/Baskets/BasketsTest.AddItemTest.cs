using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using static Web.Features.Baskets;

namespace Web.Features;
public partial class BasketsTest
{
    public class AddItemTest
    {
        [Fact]
        public async Task Should_add_the_new_item_to_the_basket()
        {
            // Arrange
            await using var application = new C18WebApplication();
            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync(
                "/baskets",
                new AddItem.Command(4, 1, 22)
            );

            // Assert the response
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            var result = await response.Content.ReadFromJsonAsync<AddItem.Response>();
            Assert.NotNull(result);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(22, result.Quantity);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<BasketContext>();
            var dbItem = db.Items.FirstOrDefault(x => x.CustomerId == 4 && x.ProductId == 1);
            Assert.NotNull(dbItem);
            Assert.Equal(22, dbItem.Quantity);
        }

        [Fact]
        public async Task Should_return_a_valid_product_url()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<Products.ProductContext>(async db =>
            {
                db.Products.RemoveRange(db.Products);
                db.Products.Add(new("A test product", 15.22m, 1));
                await db.SaveChangesAsync();
            });
            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync(
                "/baskets",
                new AddItem.Command(4, 1, 22)
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);

            var productResponse = await client.GetAsync(response.Headers.Location);
            Assert.NotNull(productResponse);
            Assert.True(productResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Should_return_a_ProblemDetails_with_a_Conflict_status_code()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<BasketContext>(async db =>
            {
                db.Items.RemoveRange(db.Items);
                db.Items.Add(new(
                    CustomerId: 1,
                    ProductId: 1,
                    Quantity: 10
                ));
                await db.SaveChangesAsync();
            });
            var client = application.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync(
                "/baskets",
                new AddItem.Command(
                    CustomerId: 1,
                    ProductId: 1,
                    Quantity: 20
                )
            );

            // Assert the response
            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            var problem = await response.Content
                .ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal("The product \u00271\u0027 is already in your shopping cart.", problem.Title);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider
                .GetRequiredService<BasketContext>();
            var dbItem = db.Items.FirstOrDefault(x => x.CustomerId == 1 && x.ProductId == 1);
            Assert.NotNull(dbItem);
            Assert.Equal(10, dbItem.Quantity);
        }
    }
}
