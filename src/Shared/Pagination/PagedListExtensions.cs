using Microsoft.EntityFrameworkCore;

namespace Shared.Pagination;

public static class PagedListEfCoreExtensions<T>
{
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize)
    {
        var totalCount = await source.CountAsync();
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, page, pageSize, totalCount);
    }
}
