using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VerticalApp;

public class FluentValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException ex)
        {
            var error = new FluentValidationObjectResultError(ex.Message, ex.Errors);
            context.Result = new BadRequestObjectResult(error);
            context.ExceptionHandled = true;
        }
    }

    public record class FluentValidationObjectResultError(string Message, IEnumerable<ValidationFailure> Errors);
}
