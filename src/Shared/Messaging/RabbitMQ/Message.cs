using System.Text.Json;

namespace Shared.Messaging.RabbitMQ;

public record Message
{
    public Message(Header header, object body)
    {
        Header = header;
        Body = JsonSerializer.Serialize(body);
    }

    public Message(Header header, string serializedObject)
    {
        Header = header;
        Body = serializedObject;
    }

    public Header Header { get; }

    public string Body { get; }
}
