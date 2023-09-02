using System.Linq.Expressions;

namespace Shared.Specifications;

public abstract class Specification<T> where T : class
{
    protected Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }

    public List<Expression<Func<T, object>>> Include { get; } = new();

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
        Include.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<T, object>> orderExpression) =>
        OrderBy = orderExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>> orderExpression) =>
        OrderByDescending = orderExpression;
}
