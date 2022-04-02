using System.ComponentModel;

namespace Sample.Identity.Domain.Enumerators
{
    public enum NotificationType
    {
        [Description("E-mail")]
        EMAIL = 1,

        [Description("SMS")]
        SMS = 2
    }
}