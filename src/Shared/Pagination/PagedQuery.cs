namespace Shared.Pagination;

public record PagedQuery
{
    public int Page { get; set; } = 0;

    public int PageSize { get; set; } = 50;
}
