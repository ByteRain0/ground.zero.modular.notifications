using System.Text;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared.Messaging.RabbitMQ;

/// <summary>
/// Register all listeners from the DI Container.
/// </summary>
public class RabbitMQReceiver
{
    private readonly RabbitMQSettings _rabbitSettings;
    private readonly IModel _channel;
    private readonly List<IListener> _listeningServices;

    public RabbitMQReceiver(IServiceProvider sp, RabbitMQSettings rabbitSettings, IModel channel)
    {
        _listeningServices = sp.GetServices<IListener>().ToList();
        _rabbitSettings = rabbitSettings;
        _channel = channel;
    }

    public void Listener(IListener service)
    {
        _channel.ExchangeDeclare(exchange: _rabbitSettings.ExchangeName,
            type: _rabbitSettings.ExchangeType);

        var queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(queue: queueName,
            exchange: _rabbitSettings.ExchangeName,
            routingKey: service.RoutingKey);
        var consumerAsync = new AsyncEventingBasicConsumer(_channel);
        consumerAsync.Received += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await service.ProcessMessage(message, ea.RoutingKey);
            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume(queue: queueName,
            autoAck: false,
            consumer: consumerAsync);
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

    public void RegisterListeners()
    {
        _listeningServices.ForEach(Listener);
    }
}
