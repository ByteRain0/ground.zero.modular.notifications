using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Push.Service.Outbox;

namespace Push.Service.Infrastructure;

public static class PushServiceInstaller
{
    public static IServiceCollection AddPushService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<PushService>();
        services.AddRabbitMQOutbox(configuration);
        return services;
    }
}
