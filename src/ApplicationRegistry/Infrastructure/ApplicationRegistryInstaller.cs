using ApplicationRegistry.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryInstaller
{
    public static IServiceCollection AddApplicationRegistryService(this IServiceCollection services)
    {
        services.AddScoped<IApplicationsRepository, ApplicationRepository>();
        services.AddScoped<IEventsRepository, EventsRepository>();
        return services;
    }
}
