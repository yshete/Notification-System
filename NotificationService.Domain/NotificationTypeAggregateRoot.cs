using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using NotificationService.Entities;

namespace NotificationService.Domain
{
    public class NotificationTypeAggregateRoot
    {
        long NotificationTypeId { get; set; }
        private NotificationType? NotificationType { get; set; }
        public NotificationTypeAggregateRoot(long notificationTypeId, NotificationType? notificationType = null)
        {
            NotificationTypeId = notificationTypeId;
            NotificationType = notificationType;
        }

        public NotificationTypeAggregateRoot(NotificationType notificationType)
        {
            NotificationType = notificationType;
        }

        public NotificationTypeAggregateRoot()
        {
        }


        public async Task<ResultFormat<NotificationType?>> GetByIdAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            var NotificationType = await unitOfWork.NotificationTypes.GetByIdAsync(NotificationTypeId, cancellationToken);
            if (NotificationType == null)
            {
                return new ResultFormat<NotificationType?> { Value = NotificationType, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Notification Type doesn't Exist" } };
            }
            return new ResultFormat<NotificationType?> { Value = NotificationType, Errors = Enumerable.Empty<string>() };
        }

        public async Task<IEnumerable<NotificationType>> GetAllAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            return await unitOfWork.NotificationTypes.GetAllAsync(cancellationToken);
        }

        public async Task<ResultFormat<long>> AddAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            if (NotificationType == null)
            {
                throw new Exception("Notification Type is not set.");
            }
            await unitOfWork.NotificationTypes.AddAsync(NotificationType, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return new ResultFormat<long> { Value = NotificationType.Id, Errors = Enumerable.Empty<string>() };
        }

        public async Task<ResultFormat<bool>> Update(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            if (NotificationType == null)
            {
                throw new Exception("Notification Type is not set.");
            }
            if (NotificationTypeId == 0)
            {
                return new ResultFormat<bool> { Value = false, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Invalid Notification Type Id" } };
            }
            var o = await unitOfWork.NotificationTypes.GetByIdAsync(NotificationTypeId, cancellationToken);
            if (o == null)
            {
                return new ResultFormat<bool> { Value = false, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Notification Type doesn't Exist" } };
            }
            unitOfWork.NotificationTypes.Update(NotificationType, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return new ResultFormat<bool> { Value = true, Errors = Enumerable.Empty<string>() };
        }

        public async Task<ResultFormat<bool>> Delete(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            if (NotificationTypeId == 0)
            {
                return new ResultFormat<bool> { Value = false, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Invalid Notification Type Id" } };
            }
            var notificationType = await unitOfWork.NotificationTypes.GetByIdAsync(NotificationTypeId, cancellationToken, false);
            if (notificationType == null)
            {
                return new ResultFormat<bool> { Value = false, HttpStatusCode = HttpStatusCode.NotFound, Errors = new string[] { "Notification Type doesn't Exist" } };
            }

            unitOfWork.NotificationTypes.Delete(notificationType, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);

            return new ResultFormat<bool> { Value = true, Errors = Enumerable.Empty<string>() };
        }

        public IEnumerable<NotificationType> Find(IUnitOfWork unitOfWork, Expression<Func<NotificationType, bool>> predicate, CancellationToken cancellationToken)
        {
            return unitOfWork.NotificationTypes.Find(predicate).ToArray();
        }
    }
}