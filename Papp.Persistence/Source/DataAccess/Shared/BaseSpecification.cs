using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
{
    public Expression<Func<TEntity, bool>> Criteria { get; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    public List<string> IncludeStrings { get; }
    public bool Tracked { get; }

    public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        this.Criteria = criteria;
        this.IncludeExpressions = new();
        this.IncludeStrings = new();
        this.Tracked = false;
    }

    protected virtual void AddInclude(Expression<Func<TEntity, object>> expression)
    {
        this.IncludeExpressions.Add(expression);
    }

    protected virtual void AddInclude(string includeString)
    {
        this.IncludeStrings.Add(includeString);
    }
}
