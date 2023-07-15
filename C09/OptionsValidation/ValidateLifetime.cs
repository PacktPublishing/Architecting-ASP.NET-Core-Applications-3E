using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
namespace OptionsValidation;
public class ValidateLifetime
{
    public class Singleton
    {
        [Fact]
        public void Should_be_validated_once()
        {
            // Arrange
            var mock = new Mock<IValidateOptions<Options>>();
            mock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()))
                .Returns(ValidateOptionsResult.Success);
            var services = new ServiceCollection();
            services.AddSingleton(mock.Object);
            services.AddOptions<Options>()
                .Configure(o => o.MyImportantProperty = "Some important value")
                .ValidateOnStart()
            ;
            var serviceProvider = services.BuildServiceProvider();

            // Act
            _ = serviceProvider.GetRequiredService<IOptionsMonitor<Options>>().CurrentValue;
            _ = serviceProvider.GetRequiredService<IOptionsMonitor<Options>>().CurrentValue;
            _ = serviceProvider.GetRequiredService<IOptionsMonitor<Options>>().CurrentValue;

            // Assert
            mock.Verify(
                x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()),
                Times.Once()
            );
        }
    }

    public class Scoped
    {
        [Fact]
        public void Should_be_validated_once()
        {
            // Arrange
            var mock = new Mock<IValidateOptions<Options>>();
            mock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()))
                .Returns(ValidateOptionsResult.Success);
            var services = new ServiceCollection();
            services.AddSingleton(mock.Object);
            services.AddOptions<Options>()
                .Configure(o => o.MyImportantProperty = "Some important value")
                .ValidateOnStart()
            ;
            var serviceProvider = services.BuildServiceProvider();
            var scope1 = serviceProvider.CreateScope();
            var scope2 = serviceProvider.CreateScope();

            // Act
            _ = scope1.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options>>().Value;
            _ = scope1.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options>>().Value;
            _ = scope2.ServiceProvider.GetRequiredService<IOptionsSnapshot<Options>>().Value;

            // Assert
            mock.Verify(
                x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()),
                Times.Exactly(2)
            );
        }
    }

    public class Transient
    {
        [Fact]
        public void Should_be_validated_once()
        {
            // Arrange
            var mock = new Mock<IValidateOptions<Options>>();
            mock.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()))
                .Returns(ValidateOptionsResult.Success);
            var services = new ServiceCollection();
            services.AddSingleton(mock.Object);
            services.AddOptions<Options>()
                .Configure(o => o.MyImportantProperty = "Some important value")
                .ValidateOnStart()
            ;
            var serviceProvider = services.BuildServiceProvider();
            var factory = serviceProvider.GetRequiredService<IOptionsFactory<Options>>();

            // Act
            _ = factory.Create("");
            _ = factory.Create("");
            _ = factory.Create("");

            // Assert
            mock.Verify(
                x => x.Validate(It.IsAny<string>(), It.IsAny<Options>()),
                Times.Exactly(3)
            );
        }
    }

    public class Options
    {
        public string? MyImportantProperty { get; set; }
    }
}
