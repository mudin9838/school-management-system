using FluentValidation;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain.Common;
using System.Net;
using System.Text.Json;

namespace SchoolManagement.Api.Middlewares;

/// <summary>
/// The purpose of this middleware is to handle exceptions that occur during the request processing and provide appropriate error responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next; // represents the next middleware in the pipeline.
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context) //The Invoke method is the entry point of the middleware.
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Errors = ex.Errors.Select(e => e.ErrorMessage)
            });
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = ex.Message
            });
        }
        catch (BusinessRuleViolationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                message = "An unexpected error occurred",
                detail = ex.Message,  // Add this for debugging
                stackTrace = ex.StackTrace // Add this for debugging (remove in production)
            }));
        }
    }
}
