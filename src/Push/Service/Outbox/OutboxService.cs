using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Messaging.RabbitMQ;

namespace Push.Service.Outbox;

internal class OutboxService
{
    private readonly OutboxDbContext _outbox;
    private readonly ILogger<OutboxService> _logger;
    private readonly IMessageSender _messageSender;

    public OutboxService(
        OutboxDbContext outbox,
        ILogger<OutboxService> logger,
        IMessageSender messageSender)
    {
        _outbox = outbox;
        _logger = logger;
        _messageSender = messageSender;
    }

    public async Task StoreMessage(Message message, string routingKey)
    {
        await _outbox.Messages.AddAsync(new OutboxMessageDataModel
        {
            Body = message.Body,
            Header = JsonSerializer.Serialize(message.Header),
            RoutingKey = routingKey,
            Id = message.Id
        });
        await _outbox.SaveChangesAsync();
    }

    public async Task ProcessExistingMessagesAsync()
    {
        var existingEvents = await _outbox
            .Messages
            .Where(x => x.ProcessedOn == null)
            .ToListAsync();

        foreach (var messageMetaData in existingEvents)
        {
            try
            {
                //TODO: check the hack with the outbox.
                var savedHeaderData = JsonSerializer.Deserialize<Header>(messageMetaData.Header)!;

                var message = new Message(
                    new Header(
                        savedHeaderData.SourceCode,
                        savedHeaderData.TenantCode,
                        savedHeaderData.EventCode),
                    messageMetaData.Body);

                _messageSender.PublishMessage(message, messageMetaData.RoutingKey);

                messageMetaData.ProcessedOn = DateTime.UtcNow;
            }
            catch (Exception e)
            {
                messageMetaData.ProcessedOn = DateTime.UtcNow;
                messageMetaData.Error = e.Message;

                _logger.LogCritical(e, "Exception occured during message relay. {Exception}", e.Message);
            }

            _outbox.Messages.Update(messageMetaData);
        }

        try
        {
            await _outbox.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e, "Exception occured saving the state of messages to the database. {Exception}",
                e.Message);
        }
    }
}
