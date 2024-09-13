using NotificationService.Domain;
using NotificationService.Entities;

namespace NotificationService.Infrastructure.DB;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IGenericRepository<Notification> Notifications { get; }
    public IGenericRepository<NotificationType> NotificationTypes { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Notifications = new GenericRepository<Notification>(_context);
        NotificationTypes = new GenericRepository<NotificationType>(_context);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
