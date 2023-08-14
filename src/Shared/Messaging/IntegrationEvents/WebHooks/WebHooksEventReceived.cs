namespace Shared.Messaging.IntegrationEvents.WebHooks;

//This might be shipped as part of a Nuget package
//I prefer having duplicate properties instead of inheritance
public record WebHooksEventReceived
{
    public object Payload { get; set; }
    public required Uri Endpoint { get; set; }
}
