using System.Net;
using System.Text.Json;

namespace LSports.DataMapping.WebApi.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = new
        {
            success = false,
            message = "An error occurred while processing your request.",
            errors = new[] { exception.Message },
            traceId = context.TraceIdentifier
        };

        context.Response.StatusCode = exception switch
        {
            ArgumentException or ArgumentNullException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError
        };

        response = exception switch
        {
            ArgumentException or ArgumentNullException => new
            {
                success = false,
                message = "Invalid request parameters.",
                errors = new[] { exception.Message },
                traceId = context.TraceIdentifier
            },
            UnauthorizedAccessException => new
            {
                success = false,
                message = "Unauthorized access.",
                errors = new[] { exception.Message },
                traceId = context.TraceIdentifier
            },
            KeyNotFoundException => new
            {
                success = false,
                message = "Resource not found.",
                errors = new[] { exception.Message },
                traceId = context.TraceIdentifier
            },
            _ => response
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}
