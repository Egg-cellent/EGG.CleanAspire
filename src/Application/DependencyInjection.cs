using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EGG.CleanAspire.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "EGG.CleanAspire.Mediator";
        });

        return services;
    }
}
