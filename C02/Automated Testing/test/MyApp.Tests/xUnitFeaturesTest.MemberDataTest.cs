using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
[Trait("group", nameof(MemberDataTest))]
public class MemberDataTest
{
    public static IEnumerable<object[]> Data => new[]
    {
            new object[] { 1, 2, false },
            new object[] { 2, 2, true },
            new object[] { 3, 3, true },
        };

    public static TheoryData<int, int, bool> TypedData => new()
        {
            { 3, 2, false },
            { 2, 3, false },
            { 5, 5, true },
        };

    [Theory]
    [MemberData(nameof(Data))]
    [MemberData(nameof(TypedData))]
    [MemberData(
            nameof(ExternalData.GetData),
            10,
            MemberType = typeof(ExternalData))]
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
        public static IEnumerable<object[]> GetData(int start) => new[]
        {
                new object[] { start, start, true },
                new object[] { start, start + 1, false },
                new object[] { start + 1, start + 1, true },
            };

        public static TheoryData<int, int, bool> TypedData => new()
            {
                { 20, 30, false },
                { 40, 50, false },
                { 50, 50, true },
            };
    }
}
}
