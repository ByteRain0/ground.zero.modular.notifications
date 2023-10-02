namespace Push.Service.Outbox;

public class OutboxMessageDataModel
{
    public string Id { get; set; }

    public required string Header { get; set; }

    public required string Body { get; set; }

    public DateTime OccuredOn { get; set; } = DateTime.UtcNow;

    public DateTime? ProcessedOn { get; set; }

    public string? Error { get; set; }

    public required string RoutingKey { get; set; }
}
