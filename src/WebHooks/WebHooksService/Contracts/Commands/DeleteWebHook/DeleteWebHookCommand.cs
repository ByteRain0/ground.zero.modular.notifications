using MediatR;

namespace WebHooks.WebHooksService.Contracts.Commands.DeleteWebHook;

public record DeleteWebHookCommand : IRequest
{
    public required Guid Id { get; init; }
}
