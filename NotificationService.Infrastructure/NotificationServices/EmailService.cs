using NotificationService.Domain;
using NotificationService.Entities;

namespace NotificationService.Infrastructure.NotificationServices
{
    public class EmailService : INotificationService
    {
        public async Task<bool> SendAsync(Notification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Email Notification Sent Successfully");
            return await Task.FromResult<bool>(true);
        }
    }
}