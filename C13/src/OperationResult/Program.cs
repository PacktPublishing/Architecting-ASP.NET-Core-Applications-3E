var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<OperationResult.SimplestForm.Executor>()
    .AddSingleton<OperationResult.SingleError.Executor>()
    .AddSingleton<OperationResult.SingleErrorWithValue.Executor>()
    .AddSingleton<OperationResult.MultipleErrorsWithValue.Executor>()
    .AddSingleton<OperationResult.WithSeverity.Executor>()
    .AddSingleton<OperationResult.StaticFactoryMethod.Executor>()
;
var app = builder.Build();
app.MapGet("/simplest-form", (OperationResult.SimplestForm.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return "Operation succeeded";
    }
    else
    {
        // Handle the failure
        return "Operation failed";
    }
});
app.MapGet("/single-error", (OperationResult.SingleError.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return "Operation succeeded";
    }
    else
    {
        // Handle the failure
        return result.ErrorMessage;
    }
});
app.MapGet("/single-error-with-value", (OperationResult.SingleErrorWithValue.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return $"Operation succeeded with a value of '{result.Value}'.";
    }
    else
    {
        // Handle the failure
        return "Operation failed";
    }
});
app.MapGet("/multiple-errors-with-value", (OperationResult.MultipleErrorsWithValue.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
        return $"Operation succeeded with a value of '{result.Value}'.";
    }
    else
    {
        // Handle the failure
        return result.Errors as object;
    }
});
app.MapGet("/multiple-errors-with-value-and-severity", (OperationResult.WithSeverity.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
    }
    else
    {
        // Handle the failure
    }
    return result;
});
app.MapGet("/static-factory-methods", (OperationResult.StaticFactoryMethod.Executor executor) =>
{
    var result = executor.Operation();
    if (result.Succeeded)
    {
        // Handle the success
    }
    else
    {
        // Handle the failure
    }
    return result;
});
app.Run();
