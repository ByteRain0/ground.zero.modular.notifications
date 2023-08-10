using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Shared.RabbitMQ;

public class MessageSender : IMessageSender
{
    private readonly IModel _channel;
    private readonly RabbitMQSettings _rabbitSettings;
    public MessageSender(RabbitMQSettings rabbitSettings, IModel channel)
    {
        _channel = channel;
        _rabbitSettings = rabbitSettings;
    }

    public void PublishMessage<T>(T entity, string key) where T : class
    {
        var message = JsonSerializer.Serialize(entity);
        //topic should become enum or similar
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: _rabbitSettings.ExchangeName,
            routingKey: key,
            basicProperties: null,
            body: body);
        Console.WriteLine(" [x] Sent '{0}':'{1}'", key, message);

    }
}
