using Microsoft.Extensions.Logging;

namespace Shared.Constants;

public static class LogEvents
{
    private const int PositiveEventsBase = 1000;

    private const int NegativeEventsBase = PositiveEventsBase * 10;

    public static (EventId EventId, string Message) CacheSaveFailure
        => (new EventId(NegativeEventsBase + 1), "Error saving cache");

}
