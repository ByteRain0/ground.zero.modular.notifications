using Shared.Messaging.RabbitMQ;

namespace EventProcessor.Service;

public class WebHooksEventReceivedHandler : IListener
{
    public WebHooksEventReceivedHandler()
    {
        RoutingKey = RoutingKeys
            .WebHooksTopic
            .ReplaceAppCodePlaceholderWith("*")
            .ReplaceTenantCodePlaceholderWith("*");
    }
    public string RoutingKey { get; }

    public Task ProcessMessage(Message message, string routingKey)
    {
        Console.WriteLine("Event received by processor and sent to client.");
        return Task.CompletedTask;
    }
}
