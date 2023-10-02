using System.Text.Json;

namespace Shared.Messaging.RabbitMQ;

public record Message
{
    public Message(Header header, object body)
    {
        Header = header;
        Body = JsonSerializer.Serialize(body);
        Id = Guid.NewGuid().ToString();
    }

    public Message(Header header, string serializedObject)
    {
        Header = header;
        Body = serializedObject;
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; }

    public Header Header { get; }

    public string Body { get; }
}
