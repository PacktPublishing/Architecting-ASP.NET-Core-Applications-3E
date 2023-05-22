using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace MyMinimalApiApp;

public class ProgramTestWithoutFixtureNoReuse
{
    public class Get : ProgramTestWithoutFixture
    {
        [Fact]
        public async Task Should_respond_a_status_200_OK()
        {
            // Arrange
            await using var webAppFactory = new WebApplicationFactory<SomeOtherClass>();
            var httpClient = webAppFactory.CreateClient();

            // Act
            var result = await httpClient.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_respond_hello_world()
        {
            // Arrange
            await using var webAppFactory = new WebApplicationFactory<SomeOtherClass>();
            var httpClient = webAppFactory.CreateClient();

            // Act
            var result = await httpClient.GetAsync("/");

            // Assert
            var contentText = await result.Content.ReadAsStringAsync();
            Assert.Equal("Hello World!", contentText);
        }
    }
}
