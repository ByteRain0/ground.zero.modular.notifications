using System.Linq.Expressions;
using FluentResults;
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

    public async Task<Result> DeleteAsync(string id)
    {
        //This try catch can be either a Decorator or some form of Middleware
        try
        {
            var deleteResult = await _context.DeleteOneAsync(id);
            return deleteResult.DeletedCount == 1
                ? Result.Ok()
                : Result.Fail(ErrorCodes.DeleteIssues);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Result.Fail(ErrorCodes.GeneralIssues);
        }
    }

    public async Task<Result<WebHook>> GetById(string id)
    {
        var webHook = await _context.Find(id).FirstOrDefaultAsync();

        if (webHook is null)
        {
            return Result.Fail(ErrorCodes.WebHookNotFound);
        }

        return webHook.ToContract();
    }

    public async Task<Result<WebHook>> GetAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var webHook = await _context
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (webHook is null)
        {
            return Result.Fail(ErrorCodes.WebHookNotFound);
        }

        return Result.Ok(webHook.ToContract());
    }

    public async Task<Result<PagedList<WebHook>>> GetListAsync(
        GetListAsyncQuery query,
        CancellationToken cancellationToken)
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

        return Result.Ok(new PagedList<WebHook>(list.Select(x => x.ToContract()).ToList(), query.Page, query.PageSize, totalCount));
    }

    public async Task<Result> SaveAsync(WebHook webHook)
    {
        var mongoDbQuery = _context
            .AsQueryable()
            .BuildSpecificationQuery(new WebHooksForApplicationWithCodeSpecification(webHook.SourceCode))
            .BuildSpecificationQuery(new WebHooksForClientWithCodeSpecification(webHook.ClientCode))
            .BuildSpecificationQuery(new WebHooksForTenantSpecification(webHook.TenantCode))
            .BuildSpecificationQuery(new WebHooksForEventWithCodeSpecification(webHook.EventCode));

        if (await mongoDbQuery.AnyAsync())
        {
            return Result.Fail(ErrorCodes.AlreadyExists);
        }

        try
        {
            await _context.InsertOneAsync(webHook.ToDataModel());
            return Result.Ok();
        }
        catch (Exception e)
        {
            //TODO: add this to logging rather.
            Console.WriteLine(e.Message);
            return Result.Fail(ErrorCodes.GeneralIssues);
        }
    }

    private static Expression<Func<WebHookDataModel, object>> GetWebHooksSortColumn(
        GetListAsyncQuery query)
    {
        return query.SortColumn?.ToLowerInvariant() switch
        {
            "eventcode" => webHook => webHook.EventCode,
            "tenantcode" => webHook => webHook.TenantCode,
            "clientcode" => webHook => webHook.ClientCode,
            "sourcecode" => webHook => webHook.SourceCode,
            _ => application => application.EventCode
        };
    }}
