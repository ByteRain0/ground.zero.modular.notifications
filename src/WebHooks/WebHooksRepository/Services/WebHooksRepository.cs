using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Pagination;
using Shared.Sorting;
using Shared.Specifications;
using WebHooks.WebHooksRepository.Contracts;
using WebHooks.WebHooksRepository.Services.Data.Mappings;
using WebHooks.WebHooksRepository.Services.Data.Models;
using WebHooks.WebHooksRepository.Services.Data.Settings;
using WebHooks.WebHooksRepository.Services.Data.Specifications;
using MongoDB.Driver.Linq;

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

    public async Task<WebHook> GetAsync(string id, CancellationToken cancellationToken)
    {
        var webHook = await _context
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        // Don't forget to mention the AsContract issue we have in the online advices.
        return webHook.ToContract();
    }

    public async Task<PagedList<WebHook>> GetListAsync(GetListAsyncQuery query, CancellationToken cancellationToken)
    {
        var mongoDbQuery = _context
            .AsQueryable()
            .BuildSpecificationQuery(new WebHooksForApplicationWithCodeSpecification(query.SourceCode))
            .BuildSpecificationQuery(new WebHooksForTenantSpecification(query.TenantCode))
            .BuildSpecificationQuery(new WebHooksForEventWithCodeSpecification(query.EventCode));

        if (query.SortOrder is not null)
        {
            if (query.SortOrder == SortOrder.Ascending)
            {
                mongoDbQuery.OrderBy(GetWebHooksSortColumn(query));
            }
            else
            {
                mongoDbQuery.OrderByDescending(GetWebHooksSortColumn(query));
            }
        }

        var totalCount = mongoDbQuery.Count();

        var list = await mongoDbQuery
            .Skip(query.PageSize * query.Page)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken:cancellationToken);

        return new PagedList<WebHook>(list.Select(x => x.ToContract()).ToList(), query.Page, query.PageSize, totalCount);
    }

    public async Task<bool> SaveAsync(WebHook webHook)
    {
        var mongoDbQuery = _context
            .AsQueryable()
            .BuildSpecificationQuery(new WebHooksForApplicationWithCodeSpecification(webHook.SourceCode))
            .BuildSpecificationQuery(new WebHooksForTenantSpecification(webHook.TenantCode))
            .BuildSpecificationQuery(new WebHooksForEventWithCodeSpecification(webHook.EventCode));

        if (await mongoDbQuery.AnyAsync())
        {
            return true;
        }

        await _context.InsertOneAsync(webHook.ToDataModel());
        return true;
    }

    private static Expression<Func<WebHookDataModel, object>> GetWebHooksSortColumn(
        GetListAsyncQuery query)
    {
        return query.SortColumn?.ToLowerInvariant() switch
        {
            "eventcode" => webhook => webhook.EventCode,
            "tenantcode" => webhook => webhook.TenantCode,
            "clientcode" => webhook => webhook.ClientCode,
            "sourcecode" => webhook => webhook.SourceCode,
            _ => application => application.EventCode
        };
    }}
