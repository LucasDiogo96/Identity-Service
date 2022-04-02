using Sample.Identity.Domain.Enumerators;
using Sample.Identity.Domain.Extensions;

namespace Sample.Identity.Domain.Common
{
    public class Notification
    {
        public string Key { get; }
        public string Message { get; }
        public string Code { get; }

        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public Notification(MappedErrorsEnum error)
        {
            Key = error.Name();
            Message = error.Description();
            Code = error.ValueAsInt().ToString();
        }
    }
}