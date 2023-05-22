using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace MyMinimalApiApp;

public class ProgramTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ProgramTest(WebApplicationFactory<Program> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    public class Get : ProgramTest
    {
        public Get(WebApplicationFactory<Program> webApplicationFactory) : base(webApplicationFactory) { }

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
