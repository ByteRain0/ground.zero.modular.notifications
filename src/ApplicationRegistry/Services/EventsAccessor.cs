using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Services;

internal class EventsAccessor : IEventsAccessor
{
    public Task<bool> CreateAsync(Event @event)
    {
        throw new NotImplementedException();
    }
}
