using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace NotificationService
{
    public interface INotificationHubService
    {
        Task<NotificationOutcome> PushNotification(string inputMessage);
    }
}
