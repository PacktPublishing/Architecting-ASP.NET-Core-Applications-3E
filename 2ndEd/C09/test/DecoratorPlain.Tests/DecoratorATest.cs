using Moq;
using Xunit;

namespace DecoratorPlain;

public class DecoratorATest
{
    public class Operation : DecoratorATest
    {
        [Fact]
        public void Should_wrap_the_decorated_result_in_a_DecoratorA_tag()
        {
            // Arrange
            var componentMock = new Mock<IComponent>();
            componentMock.Setup(x => x.Operation()).Returns("Some value");
            var sut = new DecoratorA(componentMock.Object);

            // Act
            var result = sut.Operation();

            // Assert
            Assert.Equal("<DecoratorA>Some value</DecoratorA>", result);
        }
    }
}
