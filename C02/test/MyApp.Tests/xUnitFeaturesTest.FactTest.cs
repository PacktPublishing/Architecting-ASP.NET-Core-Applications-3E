using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", "xUnit")]
    public class FactTest
    {
        [Fact]
        public void Should_be_equal()
        {
            var expectedValue = 2;
            var actualValue = 2;
            Assert.Equal(expectedValue, actualValue);
        }
    }
}
