using NotificationService.Domain;
using NotificationService.Entities;

namespace NotificationService.Infrastructure.NotificationServices
{
    public class SMSService : INotificationService
    {
        public async Task<bool> SendAsync(Notification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("SMS Notification Sent Successfully");
            return await Task.FromResult<bool>(true);
        }
    }
}