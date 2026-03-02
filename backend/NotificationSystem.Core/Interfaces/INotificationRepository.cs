using NotificationSystem.Core.Entities;

namespace NotificationSystem.Core.Interfaces;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task<Notification> AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
}
