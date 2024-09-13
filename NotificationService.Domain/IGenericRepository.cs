using System.Linq.Expressions;

namespace NotificationService.Domain;
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken, bool noTracking = true);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity, CancellationToken cancellationToken);
    void Delete(T entity, CancellationToken cancellationToken);
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
}
