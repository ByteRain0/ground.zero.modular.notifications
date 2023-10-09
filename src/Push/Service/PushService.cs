using Push.Contracts.Contract;
using Shared.Messaging;

namespace Push.Service;

internal class PushService
{
    private readonly IMessageSender _messageSender;
    public PushService(IMessageSender messageSender)
    {
        _messageSender = messageSender;
    }

    public async Task ProcessIncomingEventAsync(IncomingEvent @event)
    {
        await _messageSender.PublishMessageAsync(@event);
    }
}
