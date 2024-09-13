using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationService.Entities;

namespace NotificationService.Application.Handlers
{
    public interface INotificationHandler
    {
        Task<ResultFormat<Notification?>> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<ResultFormat<Notification?>> SendNotificationAsync(Notification notification, CancellationToken cancellationToken);
        ResultFormat<IEnumerable<Notification>> SearchNotifications(NotificationFilter notificationFilter);
    }
}