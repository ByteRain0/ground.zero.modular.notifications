using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Shared.Specifications;

public static class SpecificationQueryBuilder
{
    public static IQueryable<T> BuildSpecificationQuery<T>(
        IQueryable<T> source,
        Specification<T> specification) where T : class
    {
        var query = source;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification
            .Include
            .Aggregate(
                query,
                (current, includeSpec)
                    => current.Include(includeSpec));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        return query;
    }

    public static IMongoQueryable<T> BuildSpecificationQuery<T>(
        this IMongoQueryable<T> source,
        Specification<T> specification) where T : class
    {
        var query = source;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        return query;
    }
}
