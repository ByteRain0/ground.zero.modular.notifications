using MassTransit;
using Microsoft.Extensions.Logging;
using WebHooks.Contracts.Commands.RelayWebHook;

namespace EventProcessor.Service;

public class RelayWebHookCommandHandler : IConsumer<RelayWebHookCommand>
{
    private readonly ILogger<RelayWebHookCommandHandler> _logger;

    public RelayWebHookCommandHandler(ILogger<RelayWebHookCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<RelayWebHookCommand> context)
    {
        _logger.LogInformation("Event received by processor to send to client on Endpoint : {}");

        return Task.CompletedTask;
    }
}