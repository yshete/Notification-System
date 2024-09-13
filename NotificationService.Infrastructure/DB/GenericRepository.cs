using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NotificationService.Domain;
using NotificationService.Entities;

namespace NotificationService.Infrastructure.DB;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken)
    // {
    //     var query = _dbSet.AsQueryable();

    //     query = query.AsNoTracking();
    //     return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    //    // return await _dbSet.FindAsync(id,cancellationToken);
    // }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken, bool noTracking = true)
    {
        var query = _dbSet.AsQueryable();

        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken);
    }


    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate);
    }
}
