using MediatR;
using WebHooks.WebHooksService.Contracts.Models;

namespace WebHooks.WebHooksService.Contracts.Queries;

public record GetWebHooksQuery : IRequest<List<WebHookDto>>
{
    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TennantCode { get; set; }
}
