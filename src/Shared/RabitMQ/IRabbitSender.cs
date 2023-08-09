namespace Shared.RabitMQ;

public interface IRabbitSender
{
    void PublishMessage<T>(T entity, string key) where T : class;
}
