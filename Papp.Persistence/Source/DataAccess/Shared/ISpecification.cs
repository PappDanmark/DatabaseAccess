using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public interface ISpecification<TEntity>
{
    Expression<Func<TEntity, bool>>? Criteria { get; }
    List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
    List<string> IncludeStrings { get; }
}
