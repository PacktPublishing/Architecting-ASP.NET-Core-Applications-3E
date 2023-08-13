using System.Net.Http.Json;
using static Web.Features.Products;

namespace Web.Features;
public partial class ProductsTest
{
    public class FetchAllTest
    {
        [Fact]
        public async Task Should_return_the_products()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<ProductContext>(SeederDelegateAsync);
            var client = application.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<FetchAll.Response>("/products");

            // Assert
            Assert.NotNull(response);
            Assert.Collection(response.Products,
                p => {
                    Assert.Equal(3, p.Id);
                    Assert.Equal("Habanero Pepper", p.Name);
                },
                p => {
                    Assert.Equal(2, p.Id);
                    Assert.Equal("Scotch Bottle", p.Name);
                }
            );
        }
    }
}