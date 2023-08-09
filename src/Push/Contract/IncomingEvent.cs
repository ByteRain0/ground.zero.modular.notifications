namespace Push.Contract;

public record IncomingEvent
{
    /// <summary>
    /// Can be retrieved from the request header.
    /// </summary>
    public string TenantCode { get; init; }

    public string SourceCode { get; init; }

    public string EventCode { get; init; }

    public string Payload { get; init; }
}