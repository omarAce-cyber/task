using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;

namespace NotificationSystem.Business.Services;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;

    public DeviceService(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }

    public async Task<Device> RegisterDeviceAsync(string deviceToken, string deviceName)
    {
        var existing = await _deviceRepository.GetByTokenAsync(deviceToken);
        if (existing != null)
        {
            return existing;
        }

        var device = new Device
        {
            DeviceToken = deviceToken,
            DeviceName = deviceName,
            RegisteredAt = DateTime.UtcNow
        };

        return await _deviceRepository.AddAsync(device);
    }
}
