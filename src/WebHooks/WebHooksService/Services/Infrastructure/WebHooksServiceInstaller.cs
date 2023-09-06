using Microsoft.Extensions.DependencyInjection;
using Shared.Messaging.RabbitMQ;
using WebHooks.WebHooksService.Services.Handlers.CommandHandlers.CreateWebHook;
using WebHooks.WebHooksService.Services.Handlers.EventHandlers;

namespace WebHooks.WebHooksService.Services.Infrastructure;

public static class WebHooksServiceInstaller
{
    public static IServiceCollection AddWebHooksService(this IServiceCollection services)
    {
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(CreateWebHookCommandHandler).Assembly);
        });

        services.AddSingleton<IListener, EventReceivedListener>();
        return services;
    }
}
