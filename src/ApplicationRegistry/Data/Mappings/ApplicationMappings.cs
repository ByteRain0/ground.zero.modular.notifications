using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Data.Models;

namespace ApplicationRegistry.Data.Mappings;

internal static class ApplicationMappings
{
    internal static Application ToContract(this ApplicationDataModel source)
    {
        return new Application
        {
            Code = source.Code,
            Name = source.Name,
            Events = source.Events.Select(x => new Event
            {
                Code = x.Code,
                Name = x.Name
            }).ToList()
        };
    }

    internal static ApplicationDataModel ToDataModel(this Application source)
    {
        return new ApplicationDataModel
        {
            Code = source.Code,
            Name = source.Name,
            Events = source.Events.Select(x => new EventDataModel
            {
                Code = x.Code,
                Name = x.Name,
                ApplicationCode = source.Code
            }).ToList()
        };
    }
}
