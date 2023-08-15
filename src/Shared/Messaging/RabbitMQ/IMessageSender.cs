namespace Shared.Messaging.RabbitMQ;

public interface IMessageSender
{
    void PublishMessage(Message message, string routingKey);
}
