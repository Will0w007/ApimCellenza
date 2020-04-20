using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationService
{
    public interface INotificationHubSettings { }
    public class NotificationHubSettings : INotificationHubSettings
    {
        public string ConnectionString { get; set; }
        public string NotificationHubName { get; set; }
    }
}
