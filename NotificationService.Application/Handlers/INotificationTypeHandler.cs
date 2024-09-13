using NotificationService.Entities;

namespace NotificationService.Application.Handlers;
public interface INotificationTypeHandler
{
    Task<ResultFormat<NotificationType?>> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<NotificationType>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResultFormat<long>> AddAsync(NotificationType notificationType, CancellationToken cancellationToken);
    Task<ResultFormat<bool>> Update(long notificationTypeId, NotificationType notificationType, CancellationToken cancellationToken);
    Task<ResultFormat<bool>> Delete(long notificationTypeId, CancellationToken cancellationToken);
}
