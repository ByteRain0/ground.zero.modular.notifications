using FluentResults;
using MediatR;

namespace WebHooks.Contracts.Commands.CreateWebHook;

public record CreateWebHookCommand : IRequest<Result>
{
    public required Uri Endpoint { get; init; }

    public required string ClientCode { get; init; }

    public required string SourceCode { get; init; }

    public required string EventCode { get; init; }

    public required string TenantCode { get; init; }
}
