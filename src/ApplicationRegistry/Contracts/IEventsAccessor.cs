using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Contracts;

public interface IEventsAccessor
{
    Task<bool> CreateAsync(Event @event);
}
