using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VerticalApp;

public class FluentValidationExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ValidationException ex)
        {
            context.Result = new BadRequestObjectResult(new
            {
                ex.Message,
                ex.Errors,
            });
            context.ExceptionHandled = true;
        }
    }
}
