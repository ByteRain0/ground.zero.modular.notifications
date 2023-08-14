namespace Shared.Messaging.IntegrationEvents;

public record Header
{
    public required string AppCode { get; init; }

    public required string TenantCode { get; init; }
    
    public required DateTimeOffset DateTime { get; init; }
}
