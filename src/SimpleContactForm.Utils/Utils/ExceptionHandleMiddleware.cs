using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace SimpleContactForm.Utils.Utils;

public class ExceptionHandleMiddleware : IMiddleware
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is AggregateException aggregateException)
        {
            exception = aggregateException.GetBaseException();
        }

        return exception switch
        {
            BadRequestException httpException => HandleExceptionAsync(context, httpException, httpException.StatusCode),
            _ => HandleExceptionAsync(context, exception, HttpStatusCode.InternalServerError)
        };
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exp, HttpStatusCode code)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        return context.Response.WriteAsJsonAsync(new ErrorResponse(
                    exp.Message,
                    exp is BadRequestException httpExceptionWithErrors ? httpExceptionWithErrors.Errors : null),
                SerializerOptions);
    }
}

/// <summary>
///     Error response.
/// </summary>
/// <param name="Message">Error message.</param>
/// <param name="Errors">Error dictionary.</param>
public sealed record ErrorResponse(string Message, IDictionary<string, string?[]>? Errors)
{
}