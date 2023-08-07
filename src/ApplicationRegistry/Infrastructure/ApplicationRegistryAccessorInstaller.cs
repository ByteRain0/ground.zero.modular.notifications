using Microsoft.Extensions.DependencyInjection;

namespace ApplicationRegistry.Infrastructure;

public static class ApplicationRegistryAccessorInstaller
{
    public static IServiceCollection AddApplicationRegistryAccessor(this IServiceCollection services)
    {
        return services;
    }
}
