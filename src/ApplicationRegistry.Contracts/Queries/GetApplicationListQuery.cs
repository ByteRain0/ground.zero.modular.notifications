using Shared.Pagination;
using Shared.Sorting;

namespace ApplicationRegistry.Contracts.Queries;

public record GetApplicationListQuery : IPagedQuery, ISortedQuery
{
    public string? SortColumn { get; set; }
    public SortOrder? SortOrder { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
