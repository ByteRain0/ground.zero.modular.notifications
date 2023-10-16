using MediatR;

namespace WebHooks.Contracts.Commands.DeleteWebHook;

public record DeleteWebHookCommand : IRequest
{
    public required Guid Id { get; init; }
}
