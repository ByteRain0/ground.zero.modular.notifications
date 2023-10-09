using MassTransit;
using Push.Contracts.Contract;
using Shared.Messaging;
using WebHooks.Contracts.Commands.RelayWebHook;
using WebHooks.WebHooksRepository.Contracts;

namespace WebHooks.WebHooksService.Services.Handlers.EventHandlers;

public class EventReceivedListener : IConsumer<IncomingEvent>
{
    private readonly IWebHooksRepository _repository;

    private readonly IMessageSender _messageSender;

    public EventReceivedListener(IWebHooksRepository repository, IMessageSender messageSender)
    {
        _repository = repository;
        _messageSender = messageSender;
    }

    public async Task Consume(ConsumeContext<IncomingEvent> context)
    {
        var incomingEvent = context.Message;

        var availableWebHooks = await _repository
            .GetListAsync(
                new GetListAsyncQuery
                {
                    PageSize = int.MaxValue,
                    Page = 0,
                    TenantCode = incomingEvent.TenantCode,
                    EventCode = incomingEvent.EventCode,
                    SourceCode = incomingEvent.SourceCode
                }, CancellationToken.None);

        foreach (var availableWebHook in availableWebHooks.Items)
        {

            var command = new RelayWebHookCommand()
            {
                TenantCode = incomingEvent.TenantCode,
                EventCode = incomingEvent.EventCode,
                SourceCode = incomingEvent.SourceCode,
                Endpoint = availableWebHook.Endpoint,
                Payload = incomingEvent.Payload
            };

            await _messageSender.PublishMessageAsync(command, CancellationToken.None);
        }
    }
}
