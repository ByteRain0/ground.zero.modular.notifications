namespace Shared.Messaging.IntegrationEvents;

public record Message
{
    public required Header Header { get; init; }

    public object Body { get; set; }
}