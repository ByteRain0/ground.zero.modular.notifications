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

    public Task ProcessMessage(string message, string routingKey)
    {
        Console.WriteLine("Received event");
        return Task.CompletedTask;
    }
}
