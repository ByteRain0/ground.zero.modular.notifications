using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;

namespace ApplicationRegistry.Services;

internal class EventsRepository : IEventsRepository
{
    public Task<bool> CreateAsync(Event @event)
    {
        throw new NotImplementedException();
    }
}
