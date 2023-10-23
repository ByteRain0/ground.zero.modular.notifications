using MassTransit;
using Push.Contracts.Contract;
using Shared.Messaging;
using WebHooks.Contracts.Commands.RelayWebHook;
using WebHooks.WebHooksRepository.Contracts;
using ErrorCodes = WebHooks.Contracts.ErrorCodes;

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

        var dataRetrieval = await _repository
            .GetListAsync(
                new GetListAsyncQuery
                {
                    PageSize = int.MaxValue,
                    Page = 0,
                    TenantCode = incomingEvent.TenantCode,
                    EventCode = incomingEvent.EventCode,
                    SourceCode = incomingEvent.SourceCode
                }, CancellationToken.None);

        if (dataRetrieval.IsFailed)
        {
            throw new InvalidOperationException(ErrorCodes.GeneralModuleIssues);
        }

        foreach (var availableWebHook in dataRetrieval.Value.Items)
        {
            var command = new RelayWebHookCommand
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
