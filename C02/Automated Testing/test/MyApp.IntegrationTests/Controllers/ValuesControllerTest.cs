using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace MyApp.IntegrationTests.Controllers;

public class ValuesControllerTest : IClassFixture<WebApplicationFactory<Startup>>
{
    private readonly HttpClient _httpClient;

    public ValuesControllerTest(WebApplicationFactory<Startup> webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    public class Get : ValuesControllerTest
    {
        public Get(WebApplicationFactory<Startup> webApplicationFactory) : base(webApplicationFactory) { }

        [Fact]
        public async Task Should_respond_a_status_200_OK()
        {
            // Act
            var result = await _httpClient.GetAsync("/api/values");

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Should_respond_the_expected_strings()
        {
            // Act
            var result = await _httpClient.GetFromJsonAsync<string[]>("/api/values");

            // Assert
            Assert.Collection(result,
                x => Assert.Equal("value1", x),
                x => Assert.Equal("value2", x)
            );
        }
    }
}
