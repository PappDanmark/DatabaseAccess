using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

public class SpecificationEvaluator<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, IBaseSpecification<TEntity>? specification)
    {
        if (specification == null)
        {
            return inputQuery;
        }

        var query = specification.Tracked ? inputQuery : inputQuery.AsNoTracking();
       
        query = query.Where(specification.Criteria);
    
        query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        // Apply ordering if expressions are set.
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // To debug/print the sql queries that get executed on the db.
        // Console.WriteLine(query.ToQueryString());

        return query;
    }
}
