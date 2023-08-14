using EventProcessor.Service;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.RabbitMQ;

namespace EventProcessor.Infrastructure;

public static class EventProcessorInstaller
{
    public static IServiceCollection AddEventsProcessor(this IServiceCollection services)
    {
        services.AddSingleton<IListener, WebHooksEventReceivedHandler>();
        return services;
    }
}
