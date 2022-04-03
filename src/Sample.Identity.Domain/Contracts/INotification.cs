using Sample.Identity.Domain.Common;
using Sample.Identity.Domain.Enumerators;

namespace Sample.Identity.Domain.Contracts
{
    public interface INotification
    {
        public void AddNotification(MappedErrorsEnum error);

        public void AddNotification(string key, string message);

        public void AddNotification(Notification notification);

        public bool HasNotifications();

        public IList<Notification> GetNotifications();

        public bool Exists(MappedErrorsEnum error);
    }
}