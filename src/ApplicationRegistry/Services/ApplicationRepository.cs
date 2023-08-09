using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;

namespace ApplicationRegistry.Services;
internal class ApplicationRepository : IApplicationsRepository
{
    public Task<Application?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Application>> GetListAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
