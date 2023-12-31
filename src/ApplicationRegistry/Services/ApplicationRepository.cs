using System.Linq.Expressions;
using ApplicationRegistry.Contracts;
using ApplicationRegistry.Contracts.Models;
using ApplicationRegistry.Contracts.Queries;
using ApplicationRegistry.Data;
using ApplicationRegistry.Data.Mappings;
using ApplicationRegistry.Data.Models;
using ApplicationRegistry.Data.Specifications;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;
using Shared.Sorting;
using Shared.Specifications;

namespace ApplicationRegistry.Services;

internal class ApplicationRepository : IApplicationsRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Result<Application>> GetByCodeAsync(
        string code,
        CancellationToken cancellationToken)
    {
        var application = await SpecificationQueryBuilder
            .BuildSpecificationQuery(
                source: _context.Applications,
                specification: new ApplicationWithEventsByCodeSpecification(code))
            .FirstOrDefaultAsync(cancellationToken);

        if (application is null)
        {
            return Result.Fail(ErrorCodes.ApplicationNotFound);
        }

        return Result.Ok(application.ToContract());
    }

    public async ValueTask<Result<PagedList<Application>>> GetListAsync(
        GetApplicationListQuery query,
        CancellationToken cancellationToken)
    {
        var dbQuery = _context
            .Applications
            .Include(x => x.Events)
            .AsQueryable();

        if (query.SortOrder is not null)
        {
            if (query.SortOrder == SortOrder.Ascending)
            {
                dbQuery.OrderBy(GetApplicationSortColumn(query));
            }
            else
            {
                dbQuery.OrderByDescending(GetApplicationSortColumn(query));
            }
        }

        var list = await PagedListExtensions<Application>.CreateAsync(
            source: dbQuery.Select(x => x.ToContract()),
            page: query.Page,
            pageSize: query.PageSize,
            cancellationToken: cancellationToken);

        return Result.Ok(list);
    }

    private static Expression<Func<ApplicationDataModel, object>> GetApplicationSortColumn(
        GetApplicationListQuery query)
    {
        return query.SortColumn?.ToLowerInvariant() switch
        {
            "name" => application => application.Name,
            "code" => application => application.Code,
            _ => application => application.Name
        };
    }
}
