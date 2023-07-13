
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationState.Tests;

public class ApplicationDictionaryTest
{
    [Fact]
    public void Edge_case_where_B_overrides_A()
    {
        // Arrange
        var sp = new ServiceCollection()
            .AddSingleton<IApplicationState, ApplicationDictionary>()
            .BuildServiceProvider()
        ;

        // Step 1: Consumer A sets a string
        var consumerA = sp.GetRequiredService<IApplicationState>();
        consumerA.Set("K", "A");
        Assert.True(consumerA.Has<string>("K")); // true

        // Step 2: Consumer B overrides the value with an int
        var consumerB = sp.GetRequiredService<IApplicationState>();
        if (!consumerB.Has<int>("K")) // Oops, key K exists but it's of type string, not int
        {
            consumerB.Set("K", 123);
        }
        Assert.True(consumerB.Has<int>("K")); // true

        // Consumer A is broken!
        Assert.False(consumerA.Has<string>("K"));
    }
}