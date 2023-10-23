using FluentResults;
using MediatR;
using Shared.Pagination;
using WebHooks.Contracts.Models;
using WebHooks.Contracts.Queries.GetWebHooks;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksService.Services.Mappings;
using ErrorCodes = WebHooks.Contracts.ErrorCodes;

namespace WebHooks.WebHooksService.Services.Handlers.QueryHandlers.GetWebHooks;

internal class GetWebHooksQueryHandler : IRequestHandler<GetWebHooksQuery, Result<PagedList<WebHookDto>>>
{
    private readonly IWebHooksRepository _webHooksRepository;

    public GetWebHooksQueryHandler(IWebHooksRepository webHooksRepository)
    {
        _webHooksRepository = webHooksRepository;
    }

    public async Task<Result<PagedList<WebHookDto>>> Handle(
        GetWebHooksQuery request,
        CancellationToken cancellationToken)
    {
        var webhooksRetrievalOperation = await _webHooksRepository.GetListAsync(new GetListAsyncQuery
        {
            Page = request.Page,
            PageSize = request.PageSize,
            SortColumn = request.SortColumn,
            SortOrder = request.SortOrder,
            EventCode = request.EventCode,
            SourceCode = request.SourceCode,
            TenantCode = request.TennantCode,
        }, cancellationToken);

        if (webhooksRetrievalOperation.IsFailed)
        {
            return Result.Fail(ErrorCodes.GeneralModuleIssues);
        }

        return Result.Ok(new PagedList<WebHookDto>(
            webhooksRetrievalOperation.Value.Items.Select(x => x.ToDto()).ToList(),
            webhooksRetrievalOperation.Value.Page,
            webhooksRetrievalOperation.Value.PageSize,
            webhooksRetrievalOperation.Value.TotalCount));
    }
}
