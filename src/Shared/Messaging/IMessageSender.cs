using MassTransit;

namespace Shared.Messaging;

public interface IMessageSender
{
    Task PublishMessageAsync<T> (T message, CancellationToken cancellationToken = default) where T : class;
}

internal class MessageSender : IMessageSender
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageSender(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken) where T : class
    {
        await _publishEndpoint.Publish(message, cancellationToken);

    }
}
