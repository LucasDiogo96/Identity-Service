using System.Text.Json.Serialization;
using Sample.Identity.Domain.Common;
using Sample.Identity.Domain.Contracts;
using Sample.Identity.Domain.Enumerators;

namespace Sample.Identity.Domain.Services
{
    public class NotificationContext : INotification
    {
        private readonly List<Notification> notifications;

        [JsonIgnore]
        public IReadOnlyCollection<Notification> Notifications => notifications;

        public NotificationContext()
        {
            notifications = new List<Notification>();
        }

        public void AddNotification(MappedErrorsEnum error)
        {
            notifications.Add(new Notification(error));
        }

        public void AddNotification(string key, string message)
        {
            notifications.Add(new Notification(key, message));
        }

        public void AddNotification(Notification notification)
        {
            notifications.Add(notification);
        }

        public bool HasNotifications()
        {
            return notifications.Count > 0;
        }

        public IList<Notification> GetNotifications()
        {
            return notifications;
        }
    }
}