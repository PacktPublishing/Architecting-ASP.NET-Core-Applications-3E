using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using Xunit;
using static VerticalApp.FluentValidationExceptionFilter;

namespace VerticalApp;

public class FluentValidationExceptionFilterTest
{
    [Fact]
    public async Task Should_be_registered_in_the_pipeline_and_catch_ValidationException()
    {
        await using var application = new VerticalAppApplication(afterConfigureServices: services =>
        {
            var testAssembly = Assembly.Load("VerticalApp.Tests");
            services
                .AddValidatorsFromAssembly(testAssembly)
                .AddControllers()
                .ConfigureApplicationPartManager(p => {
                    p.ApplicationParts.Add(new AssemblyPart(testAssembly));
                })
            ;
        });
        var client = application.CreateClient();
        var httpContent = JsonContent.Create(
            new object(), // Send an empty object (no "name" property)
            options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
        );

        // Act
        var response = await client.PostAsync("/tests/ExceptionFilter", httpContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public void Should_catch_ValidationException_and_return_BadRequestObjectResult()
    {
        // Arrange
        var sut = new FluentValidationExceptionFilter();
        var actionContext = new ActionContext()
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
        var context = new ExceptionContext(actionContext, new List<IFilterMetadata>());
        var validationFailures = new ValidationFailure("PropName", "Validation message");
        context.Exception = new ValidationException("An error message", new[] { validationFailures });

        // Act
        sut.OnException(context);

        // Assert
        Assert.True(context.ExceptionHandled);
        var result = Assert.IsType<BadRequestObjectResult>(context.Result);
        var error = result.Value as FluentValidationObjectResultError;
        Assert.Equal("An error message", error.Message);
        Assert.Collection(error.Errors,
            e => {
                Assert.Equal("PropName", e.PropertyName);
                Assert.Equal("Validation message", e.ErrorMessage);
            }
        );
    }

    public record class TestEntity(string Name);

    public class Validator : AbstractValidator<TestEntity>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}

[ApiController]
[Route("tests/ExceptionFilter")]
public class FluentValidationExceptionFilterTestController
{
    [HttpPost]
    public Task<FluentValidationExceptionFilterTest.TestEntity> Execute(FluentValidationExceptionFilterTest.TestEntity entity)
        => Task.FromResult(entity);
}
