using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace OptionsValidation;

public class ValidateOptionsWithDataAnnotations
{
    [Fact]
    public void Should_pass_validation()
    {
        var services = new ServiceCollection();
        services.AddOptions<Options>()
            .Configure(o => o.MyImportantProperty = "Some important value")
            .ValidateDataAnnotations()
            .ValidateOnStart() // eager validation 
        ;
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptionsMonitor<Options>>();
        Assert.Equal("Some important value", options.CurrentValue.MyImportantProperty);
    }

    [Fact]
    public void Should_fail_validation()
    {
        var services = new ServiceCollection();
        services.AddOptions<Options>()
            .ValidateDataAnnotations()
            .ValidateOnStart() // eager validation 
        ;
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetRequiredService<IOptionsMonitor<Options>>();
        var error = Assert.Throws<OptionsValidationException>(() => options.CurrentValue);
        Assert.Collection(error.Failures,
            f => Assert.Equal("DataAnnotation validation failed for 'Options' members: 'MyImportantProperty' with the error: 'The MyImportantProperty field is required.'.", f)
        );
    }

    private class Options
    {
        [Required]
        public string? MyImportantProperty { get; set; }
    }
}
