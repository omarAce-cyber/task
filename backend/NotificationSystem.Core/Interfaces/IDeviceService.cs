using NotificationSystem.Core.Entities;

namespace NotificationSystem.Core.Interfaces;

public interface IDeviceService
{
    Task<Device> RegisterDeviceAsync(string deviceToken, string deviceName);
}
