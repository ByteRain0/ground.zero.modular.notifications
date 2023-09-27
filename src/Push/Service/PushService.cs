using Push.Contracts.Contract;
using Shared.Messaging.IntegrationEvents;
using Shared.Messaging.RabbitMQ;

namespace Push.Service;

internal class PushService
{
    private readonly IMessageSender _messageSender;

    public PushService(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public Task ProcessIncomingEventAsync(IncomingEvent @event)
    {
        var message = new Message(
            header: new Header(@event.SourceCode, @event.TenantCode, @event.EventCode),
            body: new EventReceived {Payload = @event.Payload});

        var routingKey = RoutingKeys
            .AppEventsTopic
            .ReplaceAppCodePlaceholderWith(@event.SourceCode)
            .ReplaceTenantCodePlaceholderWith(@event.TenantCode);

        _messageSender.PublishMessage(message, routingKey);

        return Task.CompletedTask;
    }
}
