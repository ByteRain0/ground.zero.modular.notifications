namespace Push.Contracts.Contract;

public interface IPushService
{
    Task RegisterIncomingEventAsync(IncomingEvent @event);
}
