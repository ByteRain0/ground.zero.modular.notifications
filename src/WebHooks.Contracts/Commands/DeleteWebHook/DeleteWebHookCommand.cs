using FluentResults;
using MediatR;

namespace WebHooks.Contracts.Commands.DeleteWebHook;

public record DeleteWebHookCommand : IRequest<Result>
{
    public required Guid Id { get; init; }
}
