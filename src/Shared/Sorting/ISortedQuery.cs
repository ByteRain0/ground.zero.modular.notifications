namespace Shared.Sorting;

public class ISortedQuery
{
    public string[] SortColumns { get; set; }

    public string SortColumn { get; set; }

    public SortOrder SortOrder { get; set; }
}
