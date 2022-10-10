using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    public List<string> IncludeStrings { get; }
    public bool Tracked { get; }

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Expression<Func<TEntity, object>>? GroupBy { get; private set; }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria, bool tracked = false)
    {
        this.Criteria = criteria;
        this.IncludeExpressions = new();
        this.IncludeStrings = new();
        this.Tracked = tracked;
        this.OrderBy = null;
        this.OrderByDescending = null;
        this.GroupBy = null;
    }

    protected virtual void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        this.IncludeExpressions.Add(expression);
    }

    protected virtual void AddInclude(string includeString)
    {
        this.IncludeStrings.Add(includeString);
    }

    protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> expression)
    {
        OrderBy = expression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> expression)
    {
        OrderByDescending = expression;
    }

    protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> expression)
    {
        GroupBy = expression;
    }
}
