using NotificationSystem.Core.Entities;

namespace NotificationSystem.Core.Interfaces;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetAllAsync();
    Task<Device?> GetByTokenAsync(string token);
    Task<Device> AddAsync(Device device);
}
