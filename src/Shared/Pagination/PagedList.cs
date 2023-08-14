using System.Text.Json.Serialization;

namespace Shared.Pagination;

public class PagedList<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; }

    [JsonPropertyName("page")]
    public int Page { get; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; }

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageSize > 1;

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => Page * PageSize < TotalCount;

    public PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
