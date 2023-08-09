namespace Shared.RabitMQ;

public interface IListener
{
    public string RoutingKey { get; }
    Task ProcessMessage (string message, string routingKey);
}
