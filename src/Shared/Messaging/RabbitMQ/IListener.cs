namespace Shared.Messaging.RabbitMQ;

public interface IListener
{
    public string RoutingKey { get; }
    Task ProcessMessage (string message, string routingKey);
}
