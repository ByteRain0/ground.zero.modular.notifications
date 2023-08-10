using ApplicationRegistry.Contracts.Models;

namespace ApplicationRegistry.Contracts;

public interface IApplicationsRepository
{
    Task<List<Application>> GetListAsync(CancellationToken cancellationToken);

    Task<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken);
}
