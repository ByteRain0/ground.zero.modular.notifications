using MediatR;

namespace WebHooks.WebHooksService.Contracts.Commands.CreateWebHook;

public record CreateWebHookCommand : IRequest
{
    public required Uri Endpoint { get; init; }

    public required string ClientCode { get; init; }

    public required string SourceCode { get; init; }

    public required string EventCode { get; init; }
    
    public required string TenantCode { get; init; }
}
