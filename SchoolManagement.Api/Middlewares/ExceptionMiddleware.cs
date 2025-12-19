using FluentValidation;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Domain.Common;
using System.Net;

namespace SchoolManagement.Api.Middlewares;

/// <summary>
/// The purpose of this middleware is to handle exceptions that occur during the request processing and provide appropriate error responses.
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next; // represents the next middleware in the pipeline.

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
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
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "An unexpected error occurred"
            });
        }
    }
}
