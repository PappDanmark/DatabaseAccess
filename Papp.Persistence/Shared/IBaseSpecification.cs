using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public interface IBaseSpecification<TEntity>
{
    Expression<Func<TEntity, bool>> Criteria { get; }
    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    List<string> IncludeStrings { get; }
    bool Tracked { get; }

    Expression<Func<TEntity, object>>? OrderBy { get; }
    Expression<Func<TEntity, object>>? OrderByDescending { get; }
    Expression<Func<TEntity, object>>? GroupBy { get; }
}
