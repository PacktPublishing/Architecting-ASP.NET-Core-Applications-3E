namespace TemplateMethod;

public class LinearSearchMachineTest
{
    public class IndexOf
    {
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 4)]
        [InlineData(3, 2)]
        [InlineData(7, null)]
        public void Should_return_the_expected_result(int value, int? expectedIndex)
        {
            // Arrange
            var sorter = new LinearSearchMachine(1, 5, 3, 4, 2);

            // Act
            var index = sorter.IndexOf(value);

            // Assert
            Assert.Equal(expectedIndex, index);
        }
    }
}
