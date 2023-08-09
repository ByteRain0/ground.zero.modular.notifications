using ApplicationRegistry.Contracts;
using ApplicationRegistry.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryAccessorInstaller
{
    public static IServiceCollection AddApplicationRegistryRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicationsRepository, ApplicationRepository>();
        services.AddScoped<IEventsRepository, EventsRepository>();
        return services;
    }
}
