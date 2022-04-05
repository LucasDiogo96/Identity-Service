namespace Sample.Identity.App.Contracts
{
    public interface INotificationService
    {
        public Task SendIdentityConfirmSms(string code, string phone);

        public Task SendRecoverySms(string code, string phone);

        public Task SendRecoveryEmail(string code, string email, string name);

        public Task SendIdentityConfirmEmail(string code, string email);
    }
}