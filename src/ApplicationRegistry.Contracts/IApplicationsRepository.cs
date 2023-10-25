using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using FluentResults;
using Shared.Pagination;

namespace ApplicationRegistry.Contracts;

public interface IApplicationsRepository
{
    ValueTask<Result<PagedList<Application>>> GetListAsync(GetApplicationListQuery query, CancellationToken cancellationToken);

    ValueTask<Result<Application>> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
