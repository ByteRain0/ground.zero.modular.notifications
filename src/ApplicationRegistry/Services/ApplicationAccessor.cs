using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Services;
internal class ApplicationAccessor : IApplicationsAccessor
{
    public Task<Application> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Application>> GetListAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
