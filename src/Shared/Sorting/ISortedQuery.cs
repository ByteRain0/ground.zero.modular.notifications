namespace Shared.Pagination;

public class ISortedQuery
{
    public string[] SortColumns { get; set; }

    public string SortColumn { get; set; }

    public SortOrder SortOrder { get; set; }
}

public enum SortOrder
{
    None = 0,
    Ascending = 1,
    Descending = 2
}
