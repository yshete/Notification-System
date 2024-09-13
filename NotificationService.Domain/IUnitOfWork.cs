using NotificationService.Entities;

namespace NotificationService.Domain;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Notification> Notifications { get; }
    IGenericRepository<NotificationType> NotificationTypes { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
