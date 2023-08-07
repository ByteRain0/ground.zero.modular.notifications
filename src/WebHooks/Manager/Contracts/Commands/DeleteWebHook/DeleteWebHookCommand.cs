using MediatR;

namespace WebHooks.Manager.Contracts.Commands.DeleteWebHook;

public record DeleteWebHookCommand : IRequest
{
    public Guid Id { get; set; }
}
