namespace Shared.RabitMQ;

public class RabbitMQSettings
{
    public string HostName { get; set; }
    public string ExchangeName { get; set; }
    public string ExchangeType { get; set; }

    // "RabbitMQSettings": {
    //     "HostName": "localhost",
    //     "ExchangeName": "notifications_exchange",
    //     "ExchangeType": "topic"
    // }
}
