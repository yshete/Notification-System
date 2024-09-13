using System.Linq.Expressions;
using NotificationService.Domain;
using NotificationService.Entities;

namespace NotificationService.Application.Handlers.Implementation;
public class NotificationTypeHandler : INotificationTypeHandler
{
    readonly IUnitOfWork unitOfWork;
    public NotificationTypeHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public virtual Task<ResultFormat<NotificationType?>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var notificationAggregateRoot = new NotificationTypeAggregateRoot(id);
        return notificationAggregateRoot.GetByIdAsync(unitOfWork, cancellationToken);
    }
    public virtual async Task<IEnumerable<NotificationType>> GetAllAsync(CancellationToken cancellationToken)
    {
        var notificationAggregateRoot = new NotificationTypeAggregateRoot();
        return await notificationAggregateRoot.GetAllAsync(unitOfWork, cancellationToken);
    }

    public virtual async Task<ResultFormat<long>> AddAsync(NotificationType notificationType, CancellationToken cancellationToken)
    {
        var notificationAggregateRoot = new NotificationTypeAggregateRoot(notificationType);
        return await notificationAggregateRoot.AddAsync(unitOfWork, cancellationToken);

    }

    public virtual async Task<ResultFormat<bool>> Update(long notificationTypeId, NotificationType notificationType, CancellationToken cancellationToken)
    {
        var notificationAggregateRoot = new NotificationTypeAggregateRoot(notificationTypeId, notificationType);
        return await notificationAggregateRoot.Update(unitOfWork, cancellationToken);
    }

    public virtual async Task<ResultFormat<bool>> Delete(long notificationTypeId, CancellationToken cancellationToken)
    {
        var notificationAggregateRoot = new NotificationTypeAggregateRoot(notificationTypeId);
        return await notificationAggregateRoot.Delete(unitOfWork, cancellationToken);
    }
    /*
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    */
}
