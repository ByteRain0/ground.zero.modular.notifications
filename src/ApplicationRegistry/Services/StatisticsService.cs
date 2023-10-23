using ApplicationRegistry.Data;
using Microsoft.EntityFrameworkCore;

namespace ApplicationRegistry.Services;

/// <summary>
/// Example of a possible background job inside a module.
/// </summary>
public class StatisticsService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public StatisticsService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task GatherStatisticsAsync()
    {
#pragma warning disable IDE0059
        var apps = await _applicationDbContext.Applications.ToListAsync();
#pragma warning restore IDE0059
    }
}
