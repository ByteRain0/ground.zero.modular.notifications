using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.Messaging.RabbitMQ;

public class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly RabbitMQReceiver _rabbitMqReceiver;
    public WorkerService(RabbitMQReceiver rabbitMqReceiver,
        ILogger<WorkerService> logger)
    {
        _logger = logger;
        _rabbitMqReceiver = rabbitMqReceiver;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Registering RabbitMQ listeners");
        _rabbitMqReceiver.RegisterListeners();
        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitMqReceiver.Dispose();
        return Task.CompletedTask;
    }
}
