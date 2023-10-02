using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Shared.Messaging.RabbitMQ;

public class MessageSender : IMessageSender
{
    private readonly IModel _channel;
    private readonly RabbitMQSettings _rabbitSettings;
    private readonly ILogger<MessageSender> _logger;

    public MessageSender(RabbitMQSettings rabbitSettings, IModel channel, ILogger<MessageSender> logger)
    {
        _rabbitSettings = rabbitSettings;
        _channel = channel;
        _logger = logger;
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

        _logger.LogInformation("Published message with key {Key} {Message}", key, message);
    }
}
