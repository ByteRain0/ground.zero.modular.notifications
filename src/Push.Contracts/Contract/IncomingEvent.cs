namespace Push.Contracts.Contract;

public record IncomingEvent
{
    public string TenantCode { get; init; }

    public string SourceCode { get; init; }

    public string EventCode { get; init; }

    public string Payload { get; init; }
}
