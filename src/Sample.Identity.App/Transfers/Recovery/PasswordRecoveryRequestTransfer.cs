using Sample.Identity.Domain.Enumerators;

namespace Sample.Identity.App.Transfers.Recovery
{
    public class PasswordRecoveryRequestTransfer
    {
        public string UserName { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}