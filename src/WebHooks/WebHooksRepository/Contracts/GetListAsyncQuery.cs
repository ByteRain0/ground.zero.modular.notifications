using Shared.Pagination;
using Shared.Sorting;

namespace WebHooks.WebHooksRepository.Contracts;

public record GetListAsyncQuery : IPagedQuery, ISortedQuery
{
    public string EventCode { get; init; }

    public string TennantCode { get; set; }

    public string SourceCode { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? SortColumn { get; set; }

    public SortOrder? SortOrder { get; set; }
}
