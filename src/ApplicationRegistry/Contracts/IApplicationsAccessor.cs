using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Contracts;

public interface IApplicationsAccessor
{
    Task<List<Application>> GetListAsync(CancellationToken cancellationToken);

    Task<Application> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
