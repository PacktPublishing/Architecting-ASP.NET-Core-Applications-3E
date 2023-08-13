using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using static Web.Features.Products;

namespace Web.Features;
public partial class ProductsTest
{
    public class FetchOneTest
    {
        [Fact]
        public async Task Should_return_the_product()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<ProductContext>(SeederDelegateAsync);
            var client = application.CreateClient();

            // Act
            var response = await client.GetFromJsonAsync<FetchOne.Response>("/products/2");

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Id);
            Assert.Equal("Scotch Bottle", response.Name);
        }

        [Fact]
        public async Task Should_return_a_ProblemDetails_with_a_NotFound_status_code()
        {
            // Arrange
            await using var application = new C18WebApplication();
            await application.SeedAsync<ProductContext>(SeederDelegateAsync);
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/products/10");

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
            Assert.Equal("The product \u002710\u0027 was not found.", problem.Title);
        }

    }
}
