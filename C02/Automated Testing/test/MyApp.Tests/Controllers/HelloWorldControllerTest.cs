using Xunit;

namespace MyApp.Controllers;

public class HelloWorldControllerTest
{
    public class Hello : HelloWorldControllerTest
    {
        [Fact]
        public void Should_return_hello_world()
        {
            // Arrange
            var sut = new HelloWorldController();

            // Act
            var result = sut.Hello();

            // Assert
            Assert.Equal("Hello World!", result);
        }
    }
}
