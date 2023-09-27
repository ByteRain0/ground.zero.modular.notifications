using MediatR;
using Shared.Pagination;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Contracts.Models;
using WebHooks.WebHooksService.Contracts.Queries.GetWebHooks;
using WebHooks.WebHooksService.Services.Mappings;

namespace WebHooks.WebHooksService.Services.Handlers.QueryHandlers.GetWebHooks;

internal class GetWebHooksQueryHandler : IRequestHandler<GetWebHooksQuery, PagedList<WebHookDto>>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public GetWebHooksQueryHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<PagedList<WebHookDto>> Handle(GetWebHooksQuery request, CancellationToken cancellationToken)
    {
        var webHooksList = await _webHooksRepository.GetListAsync(new GetListAsyncQuery
        {
            Page = request.Page,
            PageSize = request.PageSize,
            SortColumn = request.SortColumn,
            SortOrder = request.SortOrder,
            EventCode = request.EventCode,
            SourceCode = request.SourceCode,
            TenantCode = request.TennantCode,
        }, cancellationToken);

        return new PagedList<WebHookDto>(
            webHooksList.Items.Select(x => x.ToDto()).ToList(),
            webHooksList.Page,
            webHooksList.PageSize,
            webHooksList.TotalCount);
    }
}
