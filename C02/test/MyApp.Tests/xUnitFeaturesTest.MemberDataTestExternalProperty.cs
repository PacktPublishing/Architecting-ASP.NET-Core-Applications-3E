using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", nameof(MemberDataTestExternalProperty))]
    public class MemberDataTestExternalProperty
    {
        [Theory]
        [MemberData(
            nameof(ExternalData.TypedData),
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
            public static TheoryData<int, int, bool> TypedData => new()
            {
                { 20, 30, false },
                { 40, 50, false },
                { 50, 50, true },
            };
        }
    }
}
