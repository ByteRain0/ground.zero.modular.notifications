using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using Microsoft.Extensions.Logging;
using Shared.Pagination;

namespace ApplicationRegistry.CrossCutting;

public class ApplicationRepositoryLoggingDecorator : IApplicationsRepository
{
    private readonly IApplicationsRepository _next;

    private readonly ILogger<IApplicationsRepository> _logger;

    public ApplicationRepositoryLoggingDecorator(IApplicationsRepository next, ILogger<IApplicationsRepository> logger)
    {
        _next = next;
        _logger = logger;
    }

    public ValueTask<PagedList<Application>> GetListAsync(GetApplicationListQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Running GetListAsync");
        return _next.GetListAsync(query, cancellationToken);
    }

    public ValueTask<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Running GetByCodeAsync");
        return _next.GetByCodeAsync(code, cancellationToken);
    }
}
