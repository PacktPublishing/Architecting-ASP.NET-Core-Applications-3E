using System.Collections.Immutable;
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

            // Act & Assert
            Assert.Throws<NotSupportedException>(()
                => (result.Errors as ICollection<string>)?.Add("Error 2"));
        }
    }
}
