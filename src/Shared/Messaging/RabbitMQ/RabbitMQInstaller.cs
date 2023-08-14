using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Shared.Messaging.RabbitMQ;

public static class RabbitMQInstaller
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.SetUpRabbitMQ(configuration);
        services.AddSingleton<RabbitMQReceiver>();
        services.AddSingleton<IMessageSender, MessageSender>();
        return services;
    }

    private static IServiceCollection SetUpRabbitMQ(this IServiceCollection services, IConfiguration config)
    {
        // add the settings for later use by other classes via injection
        // might use IOptions pattern together with snapshots for realtime update of the settings
        var configSection = config.GetSection("RabbitMQSettings");
        var settings = new RabbitMQSettings();
        configSection.Bind(settings);
        services.AddSingleton<RabbitMQSettings>(settings);

        // As the connection factory is disposable, need to ensure container disposes of it when finished
        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            HostName = settings.HostName,
            DispatchConsumersAsync = true
        });

        services.AddSingleton<ModelFactory>();
        services.AddSingleton(sp => sp.GetRequiredService<ModelFactory>().CreateChannel());

        return services;
    }

    private class ModelFactory : IDisposable
    {
        private readonly IConnection _connection;
        private readonly RabbitMQSettings _settings;
        public ModelFactory(IConnectionFactory connectionFactory, RabbitMQSettings settings)
        {
            _settings = settings;
            _connection = connectionFactory.CreateConnection();
        }

        public IModel CreateChannel()
        {
            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: _settings.ExchangeName, type: _settings.ExchangeType);
            return channel;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }

}
