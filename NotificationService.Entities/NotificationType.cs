using System.ComponentModel.DataAnnotations;

namespace NotificationService.Entities;
public class NotificationType : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public NotificationType(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Description: {Description}";
    }
}
