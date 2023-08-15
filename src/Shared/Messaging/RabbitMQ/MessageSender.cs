using System.Text;
using RabbitMQ.Client;

namespace Shared.Messaging.RabbitMQ;

public class MessageSender : IMessageSender
{
    private readonly IModel _channel;
    private readonly RabbitMQSettings _rabbitSettings;
    public MessageSender(RabbitMQSettings rabbitSettings, IModel channel)
    {
        _channel = channel;
        _rabbitSettings = rabbitSettings;
    }

    public void PublishMessage(Message message, string key)
    {
        var properties = _channel.CreateBasicProperties();
        properties.ContentType = "text/plain";
        properties.Headers = message.Header.Properties;

        var body = Encoding.UTF8.GetBytes(message.Body);
        _channel.BasicPublish(
            exchange: _rabbitSettings.ExchangeName,
            routingKey: key,
            basicProperties: properties,
            body: body);

        Console.WriteLine(" [x] Sent '{0}':'{1}'", key, message);
    }
}
