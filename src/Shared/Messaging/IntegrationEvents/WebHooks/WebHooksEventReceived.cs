namespace Shared.Messaging.IntegrationEvents.WebHooks;

public record WebHooksEventReceived
{
    public string Payload { get; set; }
    public required Uri Endpoint { get; set; }
}
