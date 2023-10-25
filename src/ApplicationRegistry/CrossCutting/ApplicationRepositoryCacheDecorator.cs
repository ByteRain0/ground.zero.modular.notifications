using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using FluentResults;
using Shared.Cache.Distributed;
using Shared.Pagination;

namespace ApplicationRegistry.CrossCutting;

public class ApplicationRepositoryCacheDecorator : IApplicationsRepository
{
    private readonly IApplicationsRepository _next;
    private readonly ICacheService _cacheService;

    public ApplicationRepositoryCacheDecorator(IApplicationsRepository next, ICacheService cacheService)
    {
        _next = next;
        _cacheService = cacheService;
    }

    public ValueTask<Result<PagedList<Application>>> GetListAsync(
        GetApplicationListQuery query,
        CancellationToken cancellationToken)
    {
        return _next.GetListAsync(query, cancellationToken);
    }

    public async ValueTask<Result<Application>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"application_{code}";
        var cachedApplication = await _cacheService.GetValueAsync<Application>(cacheKey);

        if (cachedApplication is not null)
        {
            return Result.Ok(cachedApplication);
        }

        var application = await _next.GetByCodeAsync(code, cancellationToken);

        if (application is null)
        {
            return Result.Fail(ErrorCodes.ApplicationNotFound);
        }

        await _cacheService.SetValueAsync(cacheKey, application);

        return application;
    }
}
