using MediatR;

namespace WebHooks.Manager.Contracts.Commands.CreateWebHook;

public record CreateWebHookCommand : IRequest
{
    public Uri Endpoint { get; set; }

    public string ClientCode { get; set; }

    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TenantCode { get; set; }
}
