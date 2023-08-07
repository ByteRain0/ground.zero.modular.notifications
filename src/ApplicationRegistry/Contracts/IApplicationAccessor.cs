using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Contracts;

public interface IApplicationAccessor
{
    Task<List<Application>> GetListAsync(CancellationToken cancellationToken);

    Task<Application> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
