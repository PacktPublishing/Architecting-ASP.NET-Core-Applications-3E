using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", nameof(MemberDataTestEnumerable))]
    public class MemberDataTestEnumerable
    {
        public static IEnumerable<object[]> Data => new[]
        {
            new object[] { 1, 2, false },
            new object[] { 2, 2, true },
            new object[] { 3, 3, true },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_be_equal(int value1, int value2, bool shouldBeEqual)
        {
            if (shouldBeEqual)
            {
                Assert.Equal(value1, value2);
            }
            else
            {
                Assert.NotEqual(value1, value2);
            }
        }
    }
}
