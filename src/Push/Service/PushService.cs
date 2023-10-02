using Push.Contracts.Contract;
using Push.Service.Outbox;
using Shared.Messaging.IntegrationEvents;
using Shared.Messaging.RabbitMQ;

namespace Push.Service;

internal class PushService
{
    private readonly OutboxService _outboxService;

    public PushService(OutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task ProcessIncomingEventAsync(IncomingEvent @event)
    {
        var message = new Message(
            header: new Header(@event.SourceCode, @event.TenantCode, @event.EventCode),
            body: new EventReceived {Payload = @event.Payload});

        var routingKey = RoutingKeys
            .AppEventsTopic
            .ReplaceAppCodePlaceholderWith(@event.SourceCode)
            .ReplaceTenantCodePlaceholderWith(@event.TenantCode);

        await _outboxService.StoreMessage(message, routingKey);
    }
}
