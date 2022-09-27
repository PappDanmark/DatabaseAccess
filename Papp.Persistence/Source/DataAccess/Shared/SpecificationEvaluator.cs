using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

public class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
