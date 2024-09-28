using System.Diagnostics.CodeAnalysis;
using DesafioMottu.Application.Abstractions.Behaviors;
using DesafioMottu.Domain.Rentals;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioMottu.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

            configuration.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddTransient<PricingService>();

        return services;
    }
}
