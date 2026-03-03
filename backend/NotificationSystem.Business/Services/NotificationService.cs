using NotificationSystem.Core.Entities;
using NotificationSystem.Core.Interfaces;

namespace NotificationSystem.Business.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly INotificationLogRepository _logRepository;
    private readonly IFcmService _fcmService;

    public NotificationService(
        INotificationRepository notificationRepository,
        IDeviceRepository deviceRepository,
        INotificationLogRepository logRepository,
        IFcmService fcmService)
    {
        _notificationRepository = notificationRepository;
        _deviceRepository = deviceRepository;
        _logRepository = logRepository;
        _fcmService = fcmService;
    }

    public async Task<Notification> SendNotificationAsync(string title, string body, string sentBy)
    {
        var notification = new Notification
        {
            Title = title,
            Body = body,
            SentBy = sentBy,
            CreatedAt = DateTime.UtcNow,
            IsSent = false
        };

        notification = await _notificationRepository.AddAsync(notification);

        var devices = await _deviceRepository.GetAllAsync();
        bool overallSuccess = false;

        foreach (var device in devices)
        {
            var success = await _fcmService.SendToDeviceAsync(device.DeviceToken, title, body);
            var status = success ? "Success" : "Failed";
            overallSuccess = overallSuccess || success;

            await _logRepository.AddAsync(new NotificationLog
            {
                NotificationId = notification.Id,
                DeviceId = device.Id,
                SentAt = DateTime.UtcNow,
                Status = status
            });
        }

        // Also send to "all" topic as a fallback
        if (!devices.Any())
        {
            overallSuccess = await _fcmService.SendToAllDevicesAsync(title, body);
        }

        notification.IsSent = overallSuccess || !devices.Any();
        await _notificationRepository.UpdateAsync(notification);

        return notification;
    }

    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        => await _notificationRepository.GetAllAsync();
}
