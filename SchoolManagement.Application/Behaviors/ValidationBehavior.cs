using FluentValidation;
using MediatR;

namespace SchoolManagement.Application.Behaviors;

/// <summary>
/// This code snippet defines a ValidationBehavior class that implements the IPipelineBehavior<TRequest, TResponse> interface. It is used for validating requests before they are processed by a request handler. The behavior takes a collection of IValidator<TRequest> instances in its constructor and uses them to validate the request. If any validation errors are found, a ValidationException is thrown. Otherwise, the request is passed to the next request handler in the pipeline. The behavior is generic, allowing for different types of request and response objects. The Handle method is the entry point for the behavior and is marked as async to support asynchronous processing.
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);
        }

        return await next();
    }
}