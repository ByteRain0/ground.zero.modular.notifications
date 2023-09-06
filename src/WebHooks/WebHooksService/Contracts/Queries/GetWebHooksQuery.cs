using MediatR;
using Shared.Pagination;
using Shared.Sorting;
using WebHooks.WebHooksService.Contracts.Models;

namespace WebHooks.WebHooksService.Contracts.Queries;

public record GetWebHooksQuery : IRequest<PagedList<WebHookDto>>, IPagedQuery, ISortedQuery
{
    public string SourceCode { get; set; }

    public string EventCode { get; set; }

    public string TennantCode { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? SortColumn { get; set; }

    public SortOrder? SortOrder { get; set; }
}
