using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using static Web.Features.Baskets;

namespace Web.Features;
public partial class BasketsTest
{
    public class UpdateQuantityTest
    {
        [Fact]
        public async Task Should_update_the_item_quantity()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<BasketContext>(async db =>
            {
                db.Items.RemoveRange(db.Items.ToArray());
                db.Items.Add(new BasketItem(1, 3, 30));
                db.Items.Add(new BasketItem(2, 1, 5));
                db.Items.Add(new BasketItem(2, 3, 15));
                await db.SaveChangesAsync();
            });
            var client = application.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync(
                "/baskets",
                new UpdateQuantity.Command(2, 1, 25)
            );

            // Assert the response
            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
            var result = await response.Content.ReadFromJsonAsync<UpdateQuantity.Response>();
            Assert.NotNull(result);
            Assert.Equal(25, result.Quantity);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<BasketContext>();
            AssertProductQuantity(1, 3, 30);
            AssertProductQuantity(2, 1, 25);
            AssertProductQuantity(2, 3, 15);

            void AssertProductQuantity(int customerId, int productId, int expectedQuantity)
            {
                var dbItem = db.Items.FirstOrDefault(
                    x => x.CustomerId == customerId &&
                    x.ProductId == productId
                );
                Assert.NotNull(dbItem);
                Assert.Equal(expectedQuantity, dbItem.Quantity);
            }
        }

        [Fact]
        public async Task Should_return_a_ProblemDetails_with_a_NotFound_status_code()
        {
            // Arrange
            await using var application = new C18WebApplication();
            var client = application.CreateClient();

            // Act
            var response = await client.PutAsJsonAsync(
                "/baskets",
                new UpdateQuantity.Command(99, 99, 25)
            );

            // Assert the response
            Assert.NotNull(response);
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal("The product \u002799\u0027 is not in your shopping cart.", problem.Title);

            // Assert the database state
            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<BasketContext>();
            var dbItem = db.Items.FirstOrDefault(x => x.CustomerId == 99);
            Assert.Null(dbItem);
        }

        [Fact]
        public async Task Should_not_touch_the_database_when_the_quantity_is_the_same()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<BasketContext>(async db =>
            {
                db.Items.RemoveRange(db.Items.ToArray());
                db.Items.Add(new BasketItem(2, 1, 5));
                await db.SaveChangesAsync();
            });

            using var seedScope = application.Services.CreateScope();
            var db = seedScope.ServiceProvider
                .GetRequiredService<BasketContext>();
            var mapper = seedScope.ServiceProvider
                .GetRequiredService<UpdateQuantity.Mapper>();
            db.SavedChanges += Db_SavedChanges;
            var saved = false;

            var sut = new UpdateQuantity.Handler(db, mapper);

            // Act
            var response = await sut.HandleAsync(
                new UpdateQuantity.Command(2, 1, 5),
                CancellationToken.None
            );

            // Assert
            Assert.NotNull(response);
            Assert.False(saved);

            void Db_SavedChanges(object? sender, SavedChangesEventArgs e)
            {
                saved = true;
            }
        }
    }
}
