using System.Net.Http.Json;
using static Web.Features.Baskets;

namespace Web.Features;
public partial class BasketsTest
{
    public class FetchItemsTest
    {
        [Fact]
        public async Task Should_return_the_specified_customer_items()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<BasketContext>(async (db) =>
            {
                db.Items.RemoveRange(db.Items.ToArray());
                db.Items.Add(new BasketItem(2, 1, 5));
                db.Items.Add(new BasketItem(2, 3, 15));
                await db.SaveChangesAsync();
            });
            var client = application.CreateClient();

            // Act
            var response = await client
                .GetFromJsonAsync<IEnumerable<FetchItems.Item>>("/baskets/2");

            // Assert
            Assert.NotNull(response);
            Assert.Collection(response,
                i => {
                    Assert.Equal(1, i.ProductId);
                    Assert.Equal(5, i.Quantity);
                },
                i => {
                    Assert.Equal(3, i.ProductId);
                    Assert.Equal(15, i.Quantity);
                }
            );
        }

        [Fact]
        public async Task Should_return_an_empty_list_when_the_customer_have_no_item_in_its_cart()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<BasketContext>(async (db) =>
            {
                db.Items.RemoveRange(db.Items.ToArray());
                await db.SaveChangesAsync();
            });
            var client = application.CreateClient();

            // Act
            var response = await client
                .GetFromJsonAsync<IEnumerable<FetchItems.Item>>("/baskets/5");

            // Assert
            Assert.NotNull(response);
            Assert.Empty(response);
        }
    }
}
