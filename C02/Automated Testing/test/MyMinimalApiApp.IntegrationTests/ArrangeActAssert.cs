using Xunit;

namespace MyMinimalApiApp.IntegrationTests;

public class ArrangeActAssert
{
    [Fact]
    public void Should_be_equals()
    {
        // Arrange
        var a = 1;
        var b = 2;
        var expectedResult = 3;

        // Act
        var result = a + b;

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
