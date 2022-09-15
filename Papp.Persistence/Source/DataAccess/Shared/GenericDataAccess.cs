using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Papp.Persistence.DataAccess;

public class GenericDataAccess<T> : IGenericDataAccess<T> where T : class
{
    private readonly PappDbContext context;
    internal DbSet<T> dbSet;

    public GenericDataAccess(PappDbContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await this.context.SaveChangesAsync();
    }

    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false, string? includeProperties = null)
    {
        IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = false, string? includeProperties = null)
    {
        IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

        query = query.Where(filter);
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.FirstOrDefaultAsync();
    }
}
