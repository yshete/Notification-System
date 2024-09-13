using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotificationService.Entities;
public class Notification : BaseEntity
{
    public long UserId { get; set; }

    // Foreign Key
    public int NotificationTypeId { get; set; }

    // Navigation property for NotificationType
    [JsonIgnore]
    public NotificationType? NotificationType { get; set; }

    // Enum for Channel
    public NotificationChannel Channel { get; set; }

    public string Content { get; set; }
    public DateTime SentDate { get; set; }
    public string Status { get; set; }

    public async Task<string[]> ValidateAsync(CancellationToken cancellationToken)
    {
        List<string> validationErrors = new();
        if (UserId <= 0)
        {
            validationErrors.Add($"{nameof(UserId)} should be greater than 0");
        }
        if (NotificationTypeId <= 0)
        {
            validationErrors.Add($"{nameof(NotificationTypeId)} should be greater than 0");
        }
        if (!Enum.IsDefined(typeof(NotificationChannel), this.Channel))
        {
            validationErrors.Add("Invalid Channel");
        }
        if (NotificationType == null)
        {
            validationErrors.Add("Invalid Notification Type");
        }
        return validationErrors.ToArray();
    }
}
