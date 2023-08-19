using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using Shared.Pagination;

namespace ApplicationRegistry.Contracts;

public interface IApplicationsRepository
{
    ValueTask<PagedList<Application>> GetListAsync(GetApplicationListQuery query, CancellationToken cancellationToken);

    ValueTask<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
