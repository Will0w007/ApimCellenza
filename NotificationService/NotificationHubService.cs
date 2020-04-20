using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace NotificationService
{
    public class NotificationHubService : INotificationHubService
    {
        private readonly IOptions<NotificationHubSettings> _settings;
        public NotificationHubService(IOptions<NotificationHubSettings> settings)
        {
            _settings = settings;
        }
        public async Task<NotificationOutcome> PushNotification(string inputMessage)
        {
            NotificationHubClient hub = NotificationHubClient.CreateClientFromConnectionString(
                    _settings.Value.ConnectionString, _settings.Value.NotificationHubName);
            var FcmSampleNotificationContent = "{\"data\":{\"message\":\"Notification Hub test notification from SDK sample\"}}";
            return await hub.SendFcmNativeNotificationAsync(FcmSampleNotificationContent);
        }
    }
}
