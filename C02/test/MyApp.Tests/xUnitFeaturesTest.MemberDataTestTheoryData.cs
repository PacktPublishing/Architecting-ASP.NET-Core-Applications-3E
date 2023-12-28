using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", nameof(MemberDataTestTheoryData))]
    public class MemberDataTestTheoryData
    {
        public static TheoryData<int, int, bool> TypedData => new()
        {
            { 3, 2, false },
            { 2, 3, false },
            { 5, 5, true },
        };

        [Theory]
        [MemberData(nameof(TypedData))]
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
