using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Domain;
using NotificationService.Entities;
using NotificationService.Infrastructure.NotificationServices;

namespace NotificationService.Application.Handlers.Implementation
{
    public class NotificationHandler : INotificationHandler
    {
        IServiceProvider serviceProvider;
        private readonly IUnitOfWork unitOfWork;
        public NotificationHandler(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            this.serviceProvider = serviceProvider;
            this.unitOfWork = unitOfWork;
        }
        public virtual Task<ResultFormat<Notification?>> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var notificationAggregateRoot = new NotificationAggregateRoot(id);
            return notificationAggregateRoot.GetByIdAsync(unitOfWork, cancellationToken);
        }
        public async Task<ResultFormat<Notification?>> SendNotificationAsync(Notification notification, CancellationToken cancellationToken)
        {
            // var errors = await notification.ValidateAsync(cancellationToken);
            // if (errors.Any())
            // {
            //     return new ResultFormat<long> { Value = 0, Errors = errors };    
            // }
            INotificationService notificationService = GetNotificationService(notification.Channel);
            NotificationAggregateRoot notificationAggregateRootaggregateRoot = new NotificationAggregateRoot(notification);
            return await notificationAggregateRootaggregateRoot.SendAsync(notificationService, unitOfWork, cancellationToken);
            //return new ResultFormat<long> { Value = notification.Id, Errors = Enumerable.Empty<string>() };
        }

        public ResultFormat<IEnumerable<Notification>> SearchNotifications(NotificationFilter notificationFilter)
        {
            return NotificationAggregateRoot.Search(unitOfWork, notificationFilter);
        }

        private INotificationService GetNotificationService(NotificationChannel channel)
        {
            switch (channel)
            {
                case NotificationChannel.Email:
                    return serviceProvider.GetService<EmailService>();
                case NotificationChannel.SMS:
                    return serviceProvider.GetService<SMSService>();
                case NotificationChannel.PushNotification:
                    return serviceProvider.GetService<PushNotificationService>();
            }
            throw new InvalidEnumArgumentException(nameof(channel));
        }
    }
}