using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using ApplicationRegistry.Data;
using ApplicationRegistry.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace ApplicationRegistry.Services;

internal class ApplicationRepository : IApplicationsRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var application = await _context.Applications.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);

        if (application is null)
        {
            return default;
        }

        return application.ToContract();
    }

    public async ValueTask<PagedList<Application>> GetListAsync(GetApplicationListQuery query,
        CancellationToken cancellationToken)
    {
        return await PagedListExtensions<Application>.CreateAsync(
            source: _context.Applications.Select(x => x.ToContract()),
            page: query.Page,
            pageSize: query.PageSize);
    }
}
