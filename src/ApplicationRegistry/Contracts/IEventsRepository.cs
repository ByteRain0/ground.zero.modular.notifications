using ApplicationRegistry.Contracts.Models;

namespace ApplicationRegistry.Contracts;

public interface IEventsRepository
{
    Task<bool> CreateAsync(Event @event);
}
