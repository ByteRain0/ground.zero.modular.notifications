using FluentResults;
using MediatR;
using WebHooks.Contracts.Models;

namespace WebHooks.Contracts.Queries.GetWebHook;

public class GetWebHookQuery : IRequest<Result<WebHookDto>>
{
    public required Guid Id { get; set; }
}
