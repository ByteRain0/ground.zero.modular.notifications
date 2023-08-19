using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using Shared.Pagination;

namespace ApplicationRegistry.CrossCutting;

/// <summary>
/// TODO: add real implementation with Redis as cache, and mention it during video.
/// </summary>
public class ApplicationRepositoryCacheDecorator : IApplicationsRepository
{
    private readonly IApplicationsRepository _next;

    public ApplicationRepositoryCacheDecorator(IApplicationsRepository next)
    {
        _next = next;
    }

    public ValueTask<PagedList<Application>> GetListAsync(GetApplicationListQuery query, CancellationToken cancellationToken)
    {
        return _next.GetListAsync(query, cancellationToken);
    }

    public ValueTask<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return _next.GetByCodeAsync(code, cancellationToken);
    }
}
