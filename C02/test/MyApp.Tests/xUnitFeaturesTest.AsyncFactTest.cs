using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", "xUnit")]
    public class AsyncFactTest
    {
        [Fact]
        public async Task Should_be_equal()
        {
            var expectedValue = 2;
            var actualValue = 2;
            await Task.Yield(); // Async operation
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
