using System.Collections;
using Xunit;

namespace MyApp;

public partial class xUnitFeaturesTest
{
    [Trait("group", nameof(ClassDataTest))]
    public class ClassDataTest
    {
        [Theory]
        [ClassData(typeof(TheoryDataClass))]
        [ClassData(typeof(TheoryTypedDataClass))]
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

        public class TheoryDataClass : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, 2, false };
                yield return new object[] { 2, 2, true };
                yield return new object[] { 3, 3, true };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class TheoryTypedDataClass : TheoryData<int, int, bool>
        {
            public TheoryTypedDataClass()
            {
                Add(102, 104, false);
            }
        }
    }
}
