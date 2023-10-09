using MediatR;
using Shared.Pagination;
using Shared.Sorting;
using WebHooks.Contracts.Models;

namespace WebHooks.Contracts.Queries.GetWebHooks;

public record GetWebHooksQuery : IRequest<PagedList<WebHookDto>>, IPagedQuery, ISortedQuery
{
    public required string SourceCode { get; set; }

    public required string EventCode { get; set; }

    public required string TennantCode { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? SortColumn { get; set; }

    public SortOrder? SortOrder { get; set; }
}
