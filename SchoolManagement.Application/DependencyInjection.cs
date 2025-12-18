using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Behaviors;

namespace SchoolManagement.Application;

public static class DependencyInjection
{
    /// <summary>
    /// This code snippet is a static method named AddApplicationServices that extends the IServiceCollection interface. It adds MediatR services to the services collection by registering services from the assembly that contains the DependencyInjection class. The method then returns the updated services collection.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        return services;
    }
}
