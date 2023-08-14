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

    public async Task ProcessMessage(string message, string routingKey)
    {
        var incomingEvent = JsonSerializer.Deserialize<Message>(message);

        if (incomingEvent is null)
        {
            //TODO: treat exception here
        }

        //TODO: when implementing repository pass in additional parameters
        var availableWebHooks = await _repository.GetListAsync(CancellationToken.None);

        foreach (var availableWebHook in availableWebHooks)
        {
            var @event = new Message
            {
                Header = new Header
                {
                    AppCode = incomingEvent!.Header.AppCode,
                    TenantCode = incomingEvent.Header.TenantCode,
                    DateTime = incomingEvent.Header.DateTime
                },
                Body = new WebHooksEventReceived
                {
                    Endpoint = availableWebHook.Endpoint,
                    Payload = incomingEvent.Body
                }
            };
            _messageSender.PublishMessage(
                @event,
                RoutingKeys.WebHooksTopic
                    .ReplaceAppCodePlaceholderWith(@event.Header.AppCode)
                    .ReplaceTenantCodePlaceholderWith(@event.Header.TenantCode)
            );
        }
    }
}
