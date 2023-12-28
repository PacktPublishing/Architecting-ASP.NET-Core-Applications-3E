using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace MyApp.IntegrationTests.Controllers;

public class HelloWorldControllerTest : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _httpClient;
    public HelloWorldControllerTest(WebApplicationFactory<Startup> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }
    public class Hello : HelloWorldControllerTest
    {
        public Hello(WebApplicationFactory<Startup> webApplicationFactory)
            : base(webApplicationFactory) { }

        [Fact]
        public async Task Should_respond_a_status_200_OK()
        {
            // Act
            var result = await _httpClient.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_respond_hello_world()
        {
            // Act
            var result = await _httpClient.GetAsync("/");

            // Assert
            var contentText = await result.Content.ReadAsStringAsync();
            Assert.Equal("Hello World!", contentText);
        }
    }
}
