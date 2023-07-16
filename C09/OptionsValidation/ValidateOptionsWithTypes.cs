using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace OptionsValidation;

public class ValidateOptionsWithTypes
{
    [Fact]
    public void Should_pass_validation()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSingleton<IValidateOptions<Options>, OptionsValidator>();
        services.AddOptions<Options>()
            .Configure(o => o.MyImportantProperty = "A value")
            .ValidateOnStart()
        ;
        var serviceProvider = services.BuildServiceProvider();

        // Act & Assert
        var options = serviceProvider
            .GetRequiredService<IOptionsMonitor<Options>>();
        Assert.Equal(
            "A value",
            options.CurrentValue.MyImportantProperty
        );
    }

    [Fact]
    public void Should_fail_validation()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddSingleton<IValidateOptions<Options>, OptionsValidator>();
        services.AddOptions<Options>().ValidateOnStart();
        var serviceProvider = services.BuildServiceProvider();

        // Act & Assert
        var options = serviceProvider
            .GetRequiredService<IOptionsMonitor<Options>>();
        var error = Assert.Throws<OptionsValidationException>(
            () => options.CurrentValue);
        Assert.Collection(error.Failures,
            f => Assert.Equal("'MyImportantProperty' is required.", f)
        );
    }

    private class Options
    {
        public string? MyImportantProperty { get; set; }
    }

    private class OptionsValidator : IValidateOptions<Options>
    {
        public ValidateOptionsResult Validate(
            string name, Options options)
        {
            if (string.IsNullOrEmpty(options.MyImportantProperty))
            {
                return ValidateOptionsResult.Fail(
                    "'MyImportantProperty' is required.");
            }
            return ValidateOptionsResult.Success;
        }
    }
}
