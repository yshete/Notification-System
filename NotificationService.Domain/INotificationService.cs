using NotificationService.Entities;

namespace NotificationService.Domain;
public interface INotificationService
{
    Task<bool> SendAsync(Notification notification, CancellationToken cancellationToken);
}