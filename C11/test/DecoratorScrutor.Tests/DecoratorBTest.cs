using Moq;
using Xunit;

namespace DecoratorScrutor;

public class DecoratorBTest
{
    public class Operation : DecoratorBTest
    {
        [Fact]
        public void Should_wrap_the_decorated_result_in_a_DecoratorB_tag()
        {
            // Arrange
            var componentMock = new Mock<IComponent>();
            componentMock.Setup(x => x.Operation()).Returns("Some value");
            var sut = new DecoratorB(componentMock.Object);

            // Act
            var result = sut.Operation();

            // Assert
            Assert.Equal("<DecoratorB>Some value</DecoratorB>", result);
        }
    }
}
