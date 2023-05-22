using Xunit;

namespace MyApp.Controllers;

public class ValuesControllerTest
{
    public class Get : ValuesControllerTest
    {
        [Fact]
        public void Should_return_the_expected_strings()
        {
            // Arrange
            var sut = new ValuesController();

            // Act
            var result = sut.Get();

            // Assert
            Assert.Collection(result.Value,
                x => Assert.Equal("value1", x),
                x => Assert.Equal("value2", x)
            );
        }
    }
}
