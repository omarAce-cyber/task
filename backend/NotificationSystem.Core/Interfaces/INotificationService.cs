using NotificationSystem.Core.Entities;

namespace NotificationSystem.Core.Interfaces;

public interface INotificationService
{
    Task<Notification> SendNotificationAsync(string title, string body, string sentBy);
    Task<IEnumerable<Notification>> GetAllNotificationsAsync();
}
