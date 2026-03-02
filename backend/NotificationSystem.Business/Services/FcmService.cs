using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using NotificationSystem.Core.Interfaces;

namespace NotificationSystem.Business.Services;

public class FcmService : IFcmService
{
    public FcmService(string serviceAccountJson)
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(serviceAccountJson)
            });
        }
    }

    public async Task<bool> SendToAllDevicesAsync(string title, string body)
    {
        try
        {
            var message = new Message
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                },
                Topic = "all"
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SendToDeviceAsync(string deviceToken, string title, string body)
    {
        try
        {
            var message = new Message
            {
                Token = deviceToken,
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                }
            };

            await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
