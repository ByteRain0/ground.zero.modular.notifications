namespace Push.Contract;

public interface IPushService
{
    Task RegisterIncomingEventAsync(IncomingEvent @event);
}
