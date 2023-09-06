using System.Text.Json;
using Shared.Messaging.IntegrationEvents;
using Shared.Messaging.IntegrationEvents.WebHooks;
using Shared.Messaging.RabbitMQ;
using WebHooks.WebHooksRepository.Contracts;

namespace WebHooks.WebHooksService.Services.Handlers.EventHandlers;

public class EventReceivedListener : IListener
{
    private readonly IWebHooksRepository _repository;

    private readonly IMessageSender _messageSender;

    public string RoutingKey { get; }

    public EventReceivedListener(IWebHooksRepository repository, IMessageSender messageSender)
    {
        RoutingKey = RoutingKeys.AppEventsTopic
            .ReplaceAppCodePlaceholderWith("*")
            .ReplaceTenantCodePlaceholderWith("*");
        _repository = repository;
        _messageSender = messageSender;
    }

    public async Task ProcessMessage(Message message, string routingKey)
    {
        var incomingEvent = JsonSerializer.Deserialize<EventReceived>(message.Body);

        if (incomingEvent is null)
        {
            //TODO: Treat this issue somehow
        }

        var availableWebHooks = await _repository
            .GetListAsync(
                new GetListAsyncQuery
                {
                    PageSize = int.MaxValue,
                    Page = 0,
                    TennantCode = message.Header.TenantCode,
                    EventCode = message.Header.EventCode,
                    SourceCode = message.Header.SourceCode
                }, CancellationToken.None);

        foreach (var availableWebHook in availableWebHooks.Items)
        {
            var @event = new Message(
                header: message.Header,
                body: new WebHooksEventReceived
                {
                    Payload = incomingEvent!.Payload,
                    Endpoint = availableWebHook.Endpoint
                });

            var key = RoutingKeys.WebHooksTopic
                .ReplaceAppCodePlaceholderWith(message.Header.SourceCode!)
                .ReplaceTenantCodePlaceholderWith(message.Header.TenantCode!);

            _messageSender.PublishMessage(@event, key);
        }
    }
}
