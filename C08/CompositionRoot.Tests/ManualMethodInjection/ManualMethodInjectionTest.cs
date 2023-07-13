using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.Metrics;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompositionRoot.ManualMethodInjection;
public class ManualMethodInjectionTest
{
    // Simulating a composition root
    private static readonly IServiceProvider _serviceProvider;
    private static int _counter = 0;
    static ManualMethodInjectionTest()
    {
        _serviceProvider = new ServiceCollection()
            .AddSingleton<Subject>()
            .AddTransient(sp => new Context() { Number = _counter++ })
            .BuildServiceProvider()
        ;
    }

    // Leverage an xunit theory to get the dependencies
    // from the container and inject them in the test method.
    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]{
            _serviceProvider.GetRequiredService<Subject>(),
            _serviceProvider.GetRequiredService<Context>(),
            _counter - 1,
        };
        yield return new object[]{
            _serviceProvider.GetRequiredService<Subject>(),
            _serviceProvider.GetRequiredService<Context>(),
            _counter - 1,
        };
        yield return new object[]{
            _serviceProvider.GetRequiredService<Subject>(),
            _serviceProvider.GetRequiredService<Context>(),
            _counter - 1,
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void Showcase_manual_method_injection(Subject subject, Context context, int expectedNumber)
    {
        // Manually injecting the context into the
        // Operation method of the subject.
        var number = subject.Operation(context);

        // Validate that we got the specified context.
        Assert.Equal(expectedNumber, number);
    }

    public class Operation
    {
        [Fact]
        public void Should_return_the_value_of_the_Context_Number_property()
        {
            // Arrange
            var subject = new Subject();
            var context = new Context { Number = 44 };

            // Act
            var result = subject.Operation(context);

            // Assert
            Assert.Equal(44, result);
        }
    }
}
