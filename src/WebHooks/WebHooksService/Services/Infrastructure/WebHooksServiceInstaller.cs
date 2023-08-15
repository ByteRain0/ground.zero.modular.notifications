using Microsoft.Extensions.DependencyInjection;
using WebHooks.WebHooksService.Services.Handlers.EventHandlers;

namespace WebHooks.WebHooksService.Services.Infrastructure;

public static class WebHooksServiceInstaller
{
    public static IServiceCollection AddWebHooksService(this IServiceCollection services)
    {
        services.AddSingleton<IListener, EventReceivedListener>();
        return services;
    }
}
