using System.Linq.Expressions;

namespace Papp.Persistence.DataAccess;

public class Specification<TEntity> : BaseSpecification<TEntity>
{
    public Specification(Expression<Func<TEntity, bool>> criteria) : base(criteria)
    {
    }
}
