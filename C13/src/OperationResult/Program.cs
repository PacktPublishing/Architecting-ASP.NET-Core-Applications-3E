﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<OperationResult.SimplestForm.Executor>()
    .AddSingleton<OperationResult.SingleError.Executor>()
    .AddSingleton<OperationResult.SingleErrorWithValue.Executor>()
    .AddSingleton<OperationResult.MultipleErrorsWithValue.Executor>()
    .AddSingleton<OperationResult.WithSeverity.Executor>()
    .AddSingleton<OperationResult.StaticFactoryMethod.Executor>()
    .Configure<JsonOptions>(o
        => o.SerializerOptions.Converters.Add(
            new JsonStringEnumConverter()))
;
var app = builder.Build();
app.MapGet(
    "/simplest-form",
    (OperationResult.SimplestForm.Executor executor) =>
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
    }
);
app.MapGet(
    "/single-error",
    (OperationResult.SingleError.Executor executor) =>
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
    }
);
app.MapGet(
    "/single-error-with-value",
    (OperationResult.SingleErrorWithValue.Executor executor) =>
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
            return result.ErrorMessage;
        }
    }
);
app.MapGet(
    "/multiple-errors-with-value",
    Results<Ok<string>, BadRequest<string[]>> (OperationResult.MultipleErrorsWithValue.Executor executor) =>
    {
        var result = executor.Operation();
        if (result.Succeeded)
        {
            // Handle the success
            return TypedResults.Ok(
                $"Operation succeeded with a value of '{result.Value}'."
            );
        }
        else
        {
            // Handle the failure
            return TypedResults.BadRequest(result.Errors.ToArray());
        }
    }
);
app.MapGet(
    "/multiple-errors-with-value-and-severity",
    (OperationResult.WithSeverity.Executor executor) =>
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
    }
);
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
