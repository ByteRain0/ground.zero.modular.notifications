using ApplicationRegistry.Contracts.DTOs;

namespace ApplicationRegistry.Contracts;

public interface IEventAccessor
{
    Task<bool> CreateAsync(Event @event);
}
