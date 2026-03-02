using NotificationSystem.Core.Entities;

namespace NotificationSystem.Core.Interfaces;

public interface INotificationLogRepository
{
    Task<NotificationLog> AddAsync(NotificationLog log);
}
