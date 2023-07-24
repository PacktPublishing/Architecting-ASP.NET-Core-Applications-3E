using Microsoft.AspNetCore.Mvc.Testing;

namespace Decorator.IntegrationTests;

public class DecoratorPlainStartupTest : StartupTest<DecoratorPlain.IComponent>
{
    public DecoratorPlainStartupTest(WebApplicationFactory<DecoratorPlain.IComponent> webApplicationFactory)
        : base(webApplicationFactory)
    {
    }
}

