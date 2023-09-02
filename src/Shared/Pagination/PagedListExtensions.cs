using Microsoft.EntityFrameworkCore;

namespace Shared.Pagination;

public static class PagedListExtensions<T>
{
    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        int page,
        int pageSize,
        CancellationToken? cancellationToken = null)
    {
        cancellationToken ??= CancellationToken.None;

        var totalCount = await source.CountAsync(cancellationToken.Value);
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken.Value);

        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}
