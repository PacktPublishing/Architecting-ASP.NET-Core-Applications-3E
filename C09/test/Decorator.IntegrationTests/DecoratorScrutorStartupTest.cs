using Microsoft.AspNetCore.Mvc.Testing;

namespace Decorator.IntegrationTests;

public class DecoratorScrutorStartupTest : StartupTest<DecoratorScrutor.IComponent>
{
    public DecoratorScrutorStartupTest(WebApplicationFactory<DecoratorScrutor.IComponent> webApplicationFactory)
        : base(webApplicationFactory)
    {
    }
}
