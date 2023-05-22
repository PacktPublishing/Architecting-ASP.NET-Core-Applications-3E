using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Decorator.IntegrationTests;

public abstract class StartupTest<TStartup> : IClassFixture<WebApplicationFactory<TStartup>>
     where TStartup : class
{
    private readonly WebApplicationFactory<TStartup> _webApplicationFactory;

    protected StartupTest(WebApplicationFactory<TStartup> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory ?? throw new ArgumentNullException(nameof(webApplicationFactory));
    }

    [Fact]
    public async Task Should_return_a_double_decorated_string()
    {
        // Arrange
        var client = _webApplicationFactory.CreateClient();

        // Act
        var response = await client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal(
            "<DecoratorB><DecoratorA>Hello from ComponentA</DecoratorA></DecoratorB>",
            body
        );
    }
}
