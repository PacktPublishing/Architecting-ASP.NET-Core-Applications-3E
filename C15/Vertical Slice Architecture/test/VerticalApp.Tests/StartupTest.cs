using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace VerticalApp;

public class StartupTest
{
    [Fact]
    public async Task AutoMapper_configuration_is_valid()
    {
        // Arrange
        await using var application = new VerticalAppApplication();
        var mapper = application.Services.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
