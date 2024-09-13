using System.Net;
using NotificationService.Entities;

namespace NotificationService.Domain;
public class NotificationAggregateRoot
{
    private Notification? Notification { get; set; }
    private long NotificationId { get; set; }
    public NotificationAggregateRoot(long notificationId)
    {
        this.NotificationId = notificationId;
    }
    public NotificationAggregateRoot(Notification notification)
    {
        this.Notification = notification;
    }

    public async Task<ResultFormat<Notification?>> GetByIdAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        if (Notification == null)
        {
            Notification = await unitOfWork.Notifications.GetByIdAsync(NotificationId, cancellationToken);
            if (Notification == null)
            {
                return new ResultFormat<Notification?> { Value = this.Notification, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Notification Type doesn't Exist" } };
            }
        }
        return new ResultFormat<Notification?> { Value = Notification, Errors = Enumerable.Empty<string>() };
    }
    public async Task<ResultFormat<Notification?>> SendAsync(INotificationService notificationService, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        if (Notification == null)
        {
            throw new Exception("Notification is not set.");
        }
        Notification.SentDate = DateTime.Now;
        await LoadNotificationType(unitOfWork, cancellationToken);
        var errors = await this.Notification.ValidateAsync(cancellationToken);
        if (errors.Any())
        {
            return new ResultFormat<Notification?> { Value = null, HttpStatusCode = HttpStatusCode.BadRequest, Errors = errors };
        }
        await notificationService.SendAsync(Notification, cancellationToken);
        Notification.NotificationType = null;
        await unitOfWork.Notifications.AddAsync(Notification, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new ResultFormat<Notification?> { Value = Notification, Errors = Enumerable.Empty<string>() };
    }

    public async Task LoadNotificationType(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    {
        NotificationTypeAggregateRoot notificationTypeAggregateRoot = new NotificationTypeAggregateRoot(Notification.NotificationTypeId);
        var o = await notificationTypeAggregateRoot.GetByIdAsync(unitOfWork, cancellationToken);
        Notification.NotificationType = o.Value;
    }

    public static ResultFormat<IEnumerable<Notification>> Search(IUnitOfWork unitOfWork, NotificationFilter notificationFilter)
    {
        // Func<Notification notification,bool> predicate = (notification,bool)
        // {
        //     var result = notificationFilter.FromDate <= notification.SentDate && notificationFilter.ToDate >= notification.SentDate
        //     && (notificationFilter.UserId.HasValue ? notification.UserId == notificationFilter.UserId : true)
        //     && (notificationFilter.NotificationTypeId.HasValue ? notification.NotificationTypeId == notificationFilter.NotificationTypeId : true);
        //     return result;
        // }
        var errors = notificationFilter.Validate();
        if (errors.Any())
        {
            return new ResultFormat<IEnumerable<Notification>> { Value = Enumerable.Empty<Notification>(), HttpStatusCode = HttpStatusCode.BadRequest, Errors = errors };
        }
        // var notifications = await unitOfWork.Notifications.FindAsync((notification) => notificationFilter.FromDate <= notification.SentDate && notificationFilter.ToDate >= notification.SentDate
        //     && (notificationFilter.UserId.HasValue ? notification.UserId == notificationFilter.UserId : true)
        //     && (notificationFilter.NotificationTypeId.HasValue ? notification.NotificationTypeId == notificationFilter.NotificationTypeId : true));
        var notifications = unitOfWork.Notifications.Find((notification) => notificationFilter.FromDate <= notification.SentDate && notificationFilter.ToDate >= notification.SentDate);
        
        if(notificationFilter.UserId.HasValue)
        {
            notifications = notifications.Where(notification => notification.UserId == notificationFilter.UserId.Value);
        }

        if(notificationFilter.NotificationTypeId.HasValue)
        {
            notifications = notifications.Where(notification => notification.NotificationTypeId == notificationFilter.NotificationTypeId.Value);
        }
        var o = notifications.ToArray();
        return new ResultFormat<IEnumerable<Notification>> { Value = notifications.ToArray(), Errors = Enumerable.Empty<string>() };
    }


    // private async Task SetupMasterData(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
    // {
    //     NotificationType notificationType = new NotificationType(0, "promotional", "promotional");
    //     await unitOfWork.NotificationTypes.AddAsync(notificationType, cancellationToken);
    //     await unitOfWork.CommitAsync(cancellationToken);
    // }
}
