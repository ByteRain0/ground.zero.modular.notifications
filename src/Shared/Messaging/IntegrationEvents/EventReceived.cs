using System.Text.Json.Serialization;

namespace Shared.Messaging.IntegrationEvents;

//Class to mark the start of an integration flow.
public record EventReceived
{
    [JsonPropertyName("Payload")]
    public string Payload { get; set; }
}
