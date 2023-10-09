using EventProcessor.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EventProcessor.Infrastructure;

public static class EventProcessorInstaller
{
    public static IServiceCollection AddEventsProcessor(this IServiceCollection services)
    {
        return services;
    }
}
