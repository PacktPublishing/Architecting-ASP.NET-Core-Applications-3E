using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", nameof(MemberDataTestExternalMethod))]
    public class MemberDataTestExternalMethod
    {
        [Theory]
        [MemberData(
            nameof(ExternalData.GetData),
            10,
            MemberType = typeof(ExternalData))]
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

        public class ExternalData
        {
            public static TheoryData<int, int, bool> GetData(int start) => new()
            {
                { start, start, true },
                { start, start + 1, false },
                { start + 1, start + 1, true },
            };
        }
    }
}
