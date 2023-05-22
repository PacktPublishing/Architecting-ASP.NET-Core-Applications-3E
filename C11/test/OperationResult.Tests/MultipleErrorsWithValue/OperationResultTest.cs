using Xunit;

namespace OperationResult.MultipleErrorsWithValue;

public class OperationResultTest
{
    public class Errors : OperationResultTest
    {
        [Fact]
        public void Should_be_immutable()
        {
            // Arrange
            var result = new OperationResult("Error 1");

            // Act
            result.Errors.Add("Error 2"); // Return a mutated collection but does not change the source.

            // Assert
            Assert.Collection(result.Errors, m => Assert.Equal("Error 1", m));
        }
    }
}
