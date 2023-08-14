namespace Shared.Messaging.RabbitMQ;

public interface IMessageSender
{
    void PublishMessage<T>(T entity, string key) where T : class;
}
