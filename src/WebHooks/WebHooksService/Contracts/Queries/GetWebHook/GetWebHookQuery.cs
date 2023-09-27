using MediatR;
using WebHooks.WebHooksService.Contracts.Models;

namespace WebHooks.WebHooksService.Contracts.Queries.GetWebHook;

public class GetWebHookQuery : IRequest<WebHookDto?>
{
    public required Guid Id { get; set; }
}
