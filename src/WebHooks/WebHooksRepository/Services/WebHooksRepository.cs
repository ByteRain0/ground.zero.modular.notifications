using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Mappings;
using WebHooks.WebHooksRepository.Services.Data.Models;
using WebHooks.WebHooksRepository.Services.Data.Settings;

namespace WebHooks.WebHooksRepository.Services;

public class WebHooksRepository : IWebHooksRepository
{
    private readonly IMongoCollection<WebHookDataModel> _context;

    public WebHooksRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _context = mongoDatabase.GetCollection<WebHookDataModel>(mongoDbSettings.Value.WebHooksCollectionName);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var deleteResult = await _context.DeleteOneAsync(id);
        return deleteResult.IsAcknowledged;
    }

    public async Task<List<WebHook>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken)
    {
        var list = await _context
            .Find(x => x.EventCode.ToLowerInvariant() == query.EventCode.ToLowerInvariant())
            .ToListAsync(cancellationToken);

        return list.Select(x => x.ToContract()).ToList();
    }

    public async Task<bool> SaveAsync(WebHook webHook)
    {
        await _context.InsertOneAsync(webHook.ToDataModel());
        return true;
    }
}
