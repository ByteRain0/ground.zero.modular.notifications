using ApplicationRegistry.Contracts;
using ApplicationRegistry.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryAccessorInstaller
{
    public static IServiceCollection AddApplicationRegistryAccessor(this IServiceCollection services)
    {
        services.AddScoped<IApplicationsAccessor, ApplicationAccessor>();
        services.AddScoped<IEventsAccessor, EventsAccessor>();
        return services;
    }
}
