using Shared.Messaging.RabbitMQ;

public interface IListener
{
    public string RoutingKey { get; }

    Task ProcessMessage(Message message, string routingKey);
}
