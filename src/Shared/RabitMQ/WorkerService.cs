using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shared.RabitMQ;

public class WorkerService : BackgroundService
{
    private readonly ILogger<WorkerService> _logger;
    private readonly RabbitReceiver _rabbitReceiver;
    public WorkerService(RabbitReceiver rabbitReceiver,
        ILogger<WorkerService> logger)
    {
        _logger = logger;
        _rabbitReceiver = rabbitReceiver;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabbitReceiver.RegisterListeners();

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _rabbitReceiver.Dispose();
        return Task.CompletedTask;
    }
}
